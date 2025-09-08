using TodoApiMinimal.Models;
namespace TodoApiMinimal.Data;
public static class TodoDb
{
    public static List<Todo> Todos { get; } = new List<Todo>
    {
        new Todo { Id = 1, Title = "Walk the dog", IsCompleted = false },
        new Todo { Id = 2, Title = "Do the dishes", IsCompleted = true },
        new Todo { Id = 3, Title = "Finish this guide", IsCompleted = false }
    };
}
