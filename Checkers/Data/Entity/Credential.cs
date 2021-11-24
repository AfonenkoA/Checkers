namespace Checkers.Data.Entity;

public sealed class Credential
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Credential(string login, string password)
    {
        Login = login;
        Password = password;
    }

    public Credential()
    { }
}