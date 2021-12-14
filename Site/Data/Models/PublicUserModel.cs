using Api.Interface;
using Api.WebImplementation;
using Common.Entity;

namespace Site.Data.Models
{
    public class PublicUserModel
    {
        public int Id { get; set; }
        public string Nick { get; set; }
        public DateTime LastActivity { get; set; }
        public string PictureUrl { get; set; } = String.Empty;
        public string Type { get; set; }
        public int SocialCredit { get; set; }

        public PublicUserModel(BasicUserData data)
        {
            Id = data.Id;
            Nick = data.Nick;
            LastActivity = data.LastActivity;
            Type = data.Type.ToString();
            SocialCredit = data.SocialCredit;

        }
    }
}
