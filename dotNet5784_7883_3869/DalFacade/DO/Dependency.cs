
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="ID">include the dependency's ID</param>
/// <param name="DependentTask">describe the number of the dependend task</param>
/// <param name="previousIDTask">describe the ID's task of the previous task</param>
public record Dependency
    (
    int DependentTask,
    int previousIDTask,
    int ID = 0
    )
{
    public Dependency() : this(0, 0, 0) { }

}
