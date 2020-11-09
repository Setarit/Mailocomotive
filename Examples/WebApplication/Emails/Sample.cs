using Mailocomotive;

namespace WebApplication.Emails
{
    public class Sample : Email<string>
    {
        public Sample()
        {
            ViewPath = "Views/Email/Sample.cshtml";
        }

        public override string BuildViewModel()
        {
            return "setarit";
        }
    }
}
