
namespace DO;

/// <summary>
/// 
/// </summary>
/// <param name="ID">describes the engineer's ID number</param>
/// <param name="Name">engineer's name</param>
/// <param name="Email">engineer's email</param>
/// <param name="Level">engineer's level of job</param>
/// <param name="Cost">engineer's salary per hour</param>
public record Engineer
    (
    int ID,
    string Name,
    string Email,
    EngineerExperience Level,
    double Cost
    )
{
    public Engineer() : this(0, "", "", 0,0) { }

}
