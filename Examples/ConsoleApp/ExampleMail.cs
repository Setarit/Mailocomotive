using Mailocomotive;

namespace ConsoleApp
{
    internal class ExampleMail:Email<string>
    {
        public ExampleMail()
        {
            ViewPath = "/Email.cshtml";
        }

        public override string BuildViewModel()
        {
            return "setarit";
        }
    }
}