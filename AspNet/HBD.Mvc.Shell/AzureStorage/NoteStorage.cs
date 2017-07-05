#region using

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AzureInterfaces;
using AzureNoteEntities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

#endregion

namespace AzureStorage
{
    [Export(typeof(INoteStorage))]
    public class NoteStorage : INoteStorage
    {
        private readonly DocumentClient _client;
        private readonly FeedOptions _defaultOptions;
        private readonly object _lock = new object();

        public NoteStorage()
        {
            _client = new DocumentClient(new Uri(Constants.AzureNodeUri), Constants.AzureNodeKey);
            _defaultOptions = new FeedOptions {EnableCrossPartitionQuery = true};
        }

        public async Task<IList<Note>> GetAllAsync()
        {
            var query = AsQuery().AsDocumentQuery();

            var list = new List<Note>();

            while (query.HasMoreResults)
                list.AddRange(await query.ExecuteNextAsync<Note>());

            return list;
        }

        public Task<Note> CreateNewAsync()
        {
            return Task.FromResult(new Note {Id = Guid.NewGuid().ToString()});
        }

        public async Task<bool> SaveAsync(Note note)
        {
            ResourceResponse<Document> response;

            if (await GetByIdAsync(note.Id) == null)
            {
                note.CreatedDate = DateTime.Now;

                response = await _client.CreateDocumentAsync(
                    UriFactory.CreateDocumentCollectionUri(Constants.AzureNodeDbId, Constants.AzureNodeCollectionId)
                    , note, disableAutomaticIdGeneration: true);
            }
            else
            {
                note.UpdatedDate = DateTime.Now;

                response = await _client.ReplaceDocumentAsync(
                    UriFactory.CreateDocumentUri(Constants.AzureNodeDbId, Constants.AzureNodeCollectionId, note.Id)
                    , note);
            }

            return response.StatusCode == HttpStatusCode.Created || response.StatusCode == HttpStatusCode.OK;
        }

        public Task<bool> DeleteAsync(Note note)
        {
            return DeleteByIdAsync(note.Id);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public async Task<Note> GetByIdAsync(string id)
        {
            var item = AsQuery()
                .Where(n => n.Id == id)
                .AsDocumentQuery();

            try
            {
                return item.HasMoreResults
                    ? (await item.ExecuteNextAsync<Note>()).FirstOrDefault()
                    : null;
            }
            catch (DocumentClientException ex) when (ex.Message.Contains(
                "Entity with the specified id does not exist in the system."))
            {
                return null;
            }
        }


        public async Task<bool> DeleteByIdAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return false;

            var result = await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Constants.AzureNodeDbId,
                    Constants.AzureNodeCollectionId, id),
                new RequestOptions {PartitionKey = new PartitionKey(Undefined.Value)});

            return result.StatusCode == HttpStatusCode.NoContent;
        }

        public async Task DeleteAllAsync()
        {
            var list = (from n in await GetAllAsync() select DeleteAsync(n)).Cast<Task>().ToArray();
            Task.WaitAll(list);
        }

        protected virtual IOrderedQueryable<Note> AsQuery()
        {
            return _client.CreateDocumentQuery<Note>(
                UriFactory.CreateDocumentCollectionUri(Constants.AzureNodeDbId, Constants.AzureNodeCollectionId),
                _defaultOptions);
        }

        protected virtual void Dispose(bool isDisponding)
        {
            _client.Dispose();
        }
    }
}