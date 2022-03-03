namespace BadgeMeUp;

public interface ICurrentUserInfo
{
    public Guid GetPrincipalId();

    public string? GetPrincipalName();
}