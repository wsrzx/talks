using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System.Diagnostics;
using Microsoft.Azure.Documents.Linq;
using CodePooper.Model;

namespace CodePooper.Services
{

    public interface IDocumentDBService
    {
        Task CreateDatabaseAsync(string databaseName);

        Task CreateDocumentCollectionAsync(string databaseName, string collectionName);

        Task<List<Pooper>> GetAsync();

        Task SaveAsync(Pooper model, bool isNewItem);

        Task DeleteAsync(string id);
    }

    public class DocumentDBService : IDocumentDBService
    {
        public List<Pooper> Items { get; private set; }

        DocumentClient client;
        Uri collectionLink;

        public DocumentDBService()
        {
            client = new DocumentClient(new Uri(Constants.EndpointUri), Constants.PrimaryKey);
            collectionLink = UriFactory.CreateDocumentCollectionUri(Constants.DatabaseName, Constants.CollectionName);
        }

        public async Task CreateDatabaseAsync(string databaseName)
        {
            try
            {
                await client.CreateDatabaseIfNotExistsAsync(new Database
                {
                    Id = databaseName
                });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task CreateDocumentCollectionAsync(string databaseName, string collectionName)
        {
            try
            {
                // Create collection with 400 RU/s
                await client.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(Constants.DatabaseName),
                    new DocumentCollection
                    {
                        Id = collectionName
                    },
                    new RequestOptions
                    {
                        OfferThroughput = 400
                    });
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, id));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDocumentCollection()
        {
            try
            {
                await client.DeleteDocumentCollectionAsync(collectionLink);
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }
        }

        async Task DeleteDatabase()
        {
            try
            {
                await client.DeleteDatabaseAsync(UriFactory.CreateDatabaseUri(Constants.DatabaseName));
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }

        public async Task<List<Pooper>> GetAsync()
        {
            Items = new List<Pooper>();

            try
            {
                var query = client.CreateDocumentQuery<Pooper>(collectionLink)
                    .AsDocumentQuery();
                while (query.HasMoreResults)
                {
                    Items.AddRange(await query.ExecuteNextAsync<Pooper>());
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

            return Items;
        }

        public async Task SaveAsync(Pooper model, bool isNewItem)
        {
            try
            {
                if (isNewItem)
                {
                    await client.CreateDocumentAsync(collectionLink, model);
                }
                else
                {
                    await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(Constants.DatabaseName, Constants.CollectionName, model.Id), model);
                }
            }
            catch (DocumentClientException ex)
            {
                Debug.WriteLine("Error: ", ex.Message);
            }

        }
    }
}
