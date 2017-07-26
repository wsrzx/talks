using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodePooper.Model;
using CodePooper.Services;

namespace CodePooper
{
    public class StoreManager
    {
        IDocumentDBService documentDBService;

        public StoreManager(IDocumentDBService service)
        {
            documentDBService = service;
        }

        public Task CreateDatabase(string databaseName)
        {
            return documentDBService.CreateDatabaseAsync(databaseName);
        }

        public Task CreateDocumentCollection(string databaseName, string collectionName)
        {
            return documentDBService.CreateDocumentCollectionAsync(databaseName, collectionName);
        }

        public Task<List<Pooper>> GetStoreInfoAsync()
        {
            return documentDBService.GetAsync();
        }

        public Task SaveStoreInfoAsync(Pooper model, bool isNewItem = false)
        {
            return documentDBService.SaveAsync(model, isNewItem);
        }

        public Task DeleteStoreInfoAsync(Pooper model)
        {
            return documentDBService.DeleteAsync(model.Id);
        }

    }
}
