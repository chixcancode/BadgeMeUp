namespace BadgeMeUp
{
    public interface ICurrentUserInfo
    {
        public string? GetPrincipalName();
        public Guid GetPrincipalId();
    }
}
