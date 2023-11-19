
namespace DO;

/// <summary>
/// exception for a entity that was caled and not exist
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
///  exception for a entity that is already existed
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

/// <summary>
///exception for a entity that cant be deletied 
/// </summary>
[Serializable]
public class DalDeletionImpossible : Exception { 
    public DalDeletionImpossible(string? message) : base(message) { }
}

[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}