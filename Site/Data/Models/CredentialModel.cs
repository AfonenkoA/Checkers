using Common.Entity;
using static System.String;

namespace Site.Data.Models
{
    public class CredentialModel
    {
        public string Query { get; } = Empty;

        public CredentialModel(Credential c)
        {
            if(c.IsValid)
                Query = $"?login={c.Login}&password={c.Password}";
        }
    }
}
