namespace GB.AccessManagement.Core.Exceptions;

[Serializable]
public abstract class DomainException : Exception
{
    public string Type => GetType().Name;

    public string Title => $"A '{Type}' exception has been thrown.";

    protected DomainException(string message) : base(message) { }
}