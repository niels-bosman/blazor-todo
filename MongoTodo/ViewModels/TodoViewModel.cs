using System.ComponentModel;
using System.Runtime.CompilerServices;
using MongoTodo.Models;

namespace MongoTodo.ViewModels;

public class TodoViewModel : INotifyPropertyChanged, ITodoViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private static readonly HttpClient Http = new();
    private const string ApiBase = "https://localhost:7240/api";

    /// <summary>
    /// Whether or not a call is being made.
    /// </summary>
    private bool _isBusy;
    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// The list of Todo items to display in the front-end application.
    /// </summary>
    public List<TodoItem> TodoItemList
    {
        get
        {
            var response = Http.GetFromJsonAsync<List<TodoItem>>($"{ApiBase}/TodoItem").Result;

            return response ?? new List<TodoItem>();
        }
    }

    /// <summary>
    /// The current todo item in the form.
    /// </summary>
    private TodoItem _todoItem = new();
    public TodoItem TodoItem
    {
        get => _todoItem;
        set
        {
            _todoItem = value;
            OnPropertyChanged();
        }
    }
    
    /// <summary>
    /// Save the current todo item to the database.
    /// If the item exists, update it.
    /// </summary>
    /// <param name="todoItem"></param>
    public async void SaveToDoItem(TodoItem todoItem)
    {
        IsBusy = true;
        
        if (todoItem.Id is null)
        {
            await Http.PostAsJsonAsync($"{ApiBase}/TodoItem", todoItem);
        }
        else
        {
            await Http.PutAsJsonAsync($"{ApiBase}/TodoItem/{todoItem.Id}", todoItem);
        }
        
        OnPropertyChanged(nameof(TodoItemList));
        
        IsBusy = false;
    }

    /// <summary>
    /// Trigger a re-render in the front-end code.
    /// </summary>
    /// <param name="propertyName"></param>
    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}