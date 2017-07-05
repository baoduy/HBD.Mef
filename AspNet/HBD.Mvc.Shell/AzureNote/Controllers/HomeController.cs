#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Mvc;
using AzureInterfaces;
using AzureNoteEntities;
using HBD.Framework;

#endregion

namespace AzureNote.Controllers
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private const string CacheKey = "ListNote";
        private readonly INoteStorage _storage;

        [ImportingConstructor]
        public HomeController(INoteStorage storage)
        {
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
        public async Task<ActionResult> Index()
        {
            ViewBag.Notes = await GetNotes();
            return View();
        }

        [HttpGet]
        public ActionResult Refresh()
        {
            ClearCache();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> ViewDetails(string id)
        {
            if (id.IsNullOrEmpty())
                return RedirectToAction(nameof(Index));

            var list = await GetNotes();
            var item = list.FirstOrDefault(i => i.Id == id);
            if (item == null) return RedirectToAction(nameof(Index));

            ViewBag.Notes = list;
            return View(item);
        }


        [HttpGet]
        public async Task<ActionResult> CreateNew()
        {
            ViewBag.Notes = await GetNotes();
            var note = await _storage.CreateNewAsync();
            return View("Edit", note);
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id.IsNullOrEmpty())
                return RedirectToAction(nameof(Index));

            var item = (await GetNotes()).FirstOrDefault(i => i.Id == id);

            ViewBag.Notes = await GetNotes();
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(string id, Note item)
        {
            ViewBag.Notes = await GetNotes();

            if (!ModelState.IsValid || id != item.Id)
                return View("Edit", item);

            if (await _storage.SaveAsync(item))
            {
                ClearCache();
                return RedirectToAction(nameof(ViewDetails), new {item.Id});
            }
            ViewBag.Message = "The note cannot be saved.";
            return View("Edit", item);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id.IsNotNullOrEmpty())
            {
                await _storage.DeleteByIdAsync(id);
                ClearCache();
            }

            var list = await GetNotes();
            return list.Count > 0
                ? RedirectToAction(nameof(ViewDetails), new {list[0].Id})
                : RedirectToAction(nameof(Index));
        }
    }
}