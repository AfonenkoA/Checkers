using Common.Entity;
using static System.String;

namespace Site.Data.Models
{
    public sealed class PublicUserModel : CredentialModel
    {
        internal int Id { get; }
        internal string Nick { get; }
        internal DateTime LastActivity { get; }
        internal string PictureUrl { get; init; } = Empty;
        internal string Type { get; }
        internal int SocialCredit { get; }

        public PublicUserModel(Credential c,BasicUserData data) : base(c)
        {
            Id = data.Id;
            Nick = data.Nick;
            LastActivity = data.LastActivity;
            Type = data.Type.ToString();
            SocialCredit = data.SocialCredit;

        }
    }
}
