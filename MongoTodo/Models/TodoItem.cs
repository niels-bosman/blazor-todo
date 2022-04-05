namespace MongoTodo.Models;

public class TodoItem
{
    public string? Id { get; set; }
    public string? Title { get; set; }
    public DateTime? Date { get; set; }
    public bool Finished { get; set; }
}