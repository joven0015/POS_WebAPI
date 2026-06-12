namespace POS.Models
{
    public class User
    {
        public System.Guid id { get; set; }

        public string email { get; set; } = "";

        public string password_hash { get; set; } = "";

        public string role { get; set; } = "";

        public DateTime created_at { get; set; }
    }
}
