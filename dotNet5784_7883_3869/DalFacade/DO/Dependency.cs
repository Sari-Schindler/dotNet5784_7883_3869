
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="ID"></param>
/// <param name="DependentTask"></param>
/// <param name="DependsOnTask"></param>
public record Dependency
    (
    int DependentTask,//המשימות תלויות אחת בשניה
    int previousIDTask,//מס' מזהה של משימה קודמת
    int ID=0
    )
{
}
