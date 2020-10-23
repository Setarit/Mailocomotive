namespace Mailocomotive.Setting.Single
{
    public class SmtpMailProvider:MailProvider
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool Ssl { get; set; }
    }
}
