using System.ComponentModel;
using MongoTodo.Models;

namespace MongoTodo.ViewModels;

public interface ITodoViewModel
{
    bool IsBusy { get; }
    TodoItem TodoItem { get; set; }
    List<TodoItem> TodoItemList { get; }

    event PropertyChangedEventHandler PropertyChanged;

    void SaveToDoItem(TodoItem todoItem);
}