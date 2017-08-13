using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using AzureInterfaces;
using AzureNoteEntities;
using HBD.Framework;
using HBD.Framework.Core;

namespace AzureNotesApi.Controllers
{
    public class NotesController : ApiController
    {
        private const string CacheKey = "ListNote";
        private readonly INoteStorage _storage;

        [ImportingConstructor]
        public NotesController(INoteStorage storage)
        {
            Guard.ArgumentIsNotNull(storage, nameof(storage));
            _storage = storage;
        }

        private async Task<IList<Note>> GetNotes()
        {
            var list = MemoryCache.Default.Contains(CacheKey) ? MemoryCache.Default.Get(CacheKey) as IList<Note> : null;
            if (list != null) return list;

            list = await _storage.GetAllAsync();
            list = list.OrderByDescending(a => a.UpdatedDate ?? a.CreatedDate).ToList();

            MemoryCache.Default.Add(CacheKey, list, DateTimeOffset.Now.Add(new TimeSpan(10, 0, 0)));
            return list;
        }

        private static void ClearCache()
        {
            MemoryCache.Default.Remove(CacheKey);
        }

        [HttpGet]
        public Task<IList<Note>> Get()
        {
            return GetNotes();
        }

        [HttpGet]
        public async Task<Note> Get(string id)
        {
            var list = await GetNotes();
            return list.FirstOrDefault(i => i.Id == id);
        }

        private async Task<IHttpActionResult> Save(string id, [FromBody] Note note)
        {
            if (!ModelState.IsValid || id != note.Id)
                return BadRequest(ModelState);

            if (await _storage.SaveAsync(note))
            {
                ClearCache();
                return Ok();
            }

            return new InternalServerErrorResult(Request);
        }


        [HttpPost]
        public Task<IHttpActionResult> Post([FromBody] Note note)
        {
            return Save(note.Id, note);
        }

        [HttpPut]
        public Task<IHttpActionResult> Put(string id, [FromBody] Note note)
        {
            return Save(id, note);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (id.IsNullOrEmpty())
                return BadRequest("Id is empty.");

            await _storage.DeleteByIdAsync(id);
            ClearCache();
            return Ok();
        }
    }
}