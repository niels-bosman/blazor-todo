namespace TodoApi.Models;

public class TodoItemDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string TodoCollectionName { get; set; } = null!;
}