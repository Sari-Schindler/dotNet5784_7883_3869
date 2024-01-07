

using BlImplementation;

namespace BO;
/// <summary>
/// exception for an entity that is wanted but does't exist
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}


/// <summary>
/// exception for an invalid value 
/// </summary>
[Serializable]
public class BlInvalidValueException : Exception
{
    public BlInvalidValueException(string? message) : base(message) { }
}


/// <summary>
/// exception for an entity that already exist
/// </summary>

[Serializable]
public class BlAlreadyExistsException: Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}

/// <summary>
/// exception for an entity that can't be deleted
/// </summary>
[Serializable]
public class BlCannotBeDeletedException : Exception
{
    public BlCannotBeDeletedException(string? message) : base(message) { }
}








