using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService
{
   private readonly IMongoCollection<TodoItem> _todoItemCollection;

   /// <summary>
   /// Instantiate the service
   /// </summary>
   /// <param name="todoItemDatabaseSettings"></param>
   public TodoService(IOptions<TodoItemDatabaseSettings> todoItemDatabaseSettings)
   {
      var mongoClient = new MongoClient(todoItemDatabaseSettings.Value.ConnectionString);
      var mongoDatabase = mongoClient.GetDatabase(todoItemDatabaseSettings.Value.DatabaseName);

      //!!! CollectionName
      _todoItemCollection = mongoDatabase.GetCollection<TodoItem>(todoItemDatabaseSettings.Value.TodoCollectionName);
   }

   /// <summary>
   /// Get all of the items.
   /// </summary>
   /// <returns></returns>
   public async Task<List<TodoItem>> GetAsync() => await _todoItemCollection.Find(_ => true).ToListAsync();
   
   /// <summary>
   /// Get a specific item.
   /// </summary>
   /// <param name="id"></param>
   /// <returns></returns>
   public async Task<TodoItem?> GetAsync(string id)
   {
      return await _todoItemCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
   }

   /// <summary>
   /// Create an item
   /// </summary>
   /// <param name="todoItem"></param>
   public async Task CreateAsync(TodoItem todoItem) => await _todoItemCollection.InsertOneAsync(todoItem);

   /// <summary>
   /// Update an item
   /// </summary>
   /// <param name="id"></param>
   /// <param name="todoItem"></param>
   public async Task UpdateAsync(string id, TodoItem todoItem)
   { 
      await _todoItemCollection.ReplaceOneAsync(x => x.Id == id, todoItem);
   }

   /// <summary>
   /// Remove an item
   /// </summary>
   /// <param name="id"></param>
   public async Task RemoveAsync(string id) => await _todoItemCollection.DeleteOneAsync(x => x.Id == id);
}