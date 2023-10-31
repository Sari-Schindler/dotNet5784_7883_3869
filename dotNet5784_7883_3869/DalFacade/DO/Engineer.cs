
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="ID"></param>
/// <param name="Name"></param>
/// <param name="Email"></param>
/// <param name="Level"></param>
/// <param name="Cost"></param>
public record Engineer
    (
    int ID,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost
    )
{

}
