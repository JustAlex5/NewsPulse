namespace CommonExtensionsInterface.Models
{
    public class ConnectionStringModel
    {
        public int Id { get; set; }
        public string Connection { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string DbType { get; set; }
    }
}
