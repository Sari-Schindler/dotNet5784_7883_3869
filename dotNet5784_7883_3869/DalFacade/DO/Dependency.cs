
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="ID"></param>
/// <param name="DependentTask"></param>
/// <param name="DependsOnTask"></param>
public record Dependency
    (
    int ID,
    int DependentTask,
    int DependsOnTask
    )
{
}
