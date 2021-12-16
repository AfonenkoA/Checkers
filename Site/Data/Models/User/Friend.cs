using Common.Entity;
using static System.String;

namespace Site.Data.Models.User;

public class Friend : UserInfo
{
    public string ChatUrl { get; init; } = Empty;
    public Friend( BasicUserData data) : base( data)
    { }
}