using Mailocomotive.Configuration;
using Mailocomotive.Setting.Single;
using System;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Sending");
            var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            var provider = new SmtpMailProvider()
            {
                DisplayName = "Demo",
                Host = "smtp.mailtrap.io",
                Port = 2525,
                Ssl = true,
                Username = "yourusername",
                Password = "yourpassword"
            };
            Console.WriteLine("Configuration setting up");
            Api.Configuration()
                //.UseProjectRoot(path)
                .UseProvider(provider);
            var email = new ExampleMail();
            Console.WriteLine("Printing contents of email in terminal");
            Console.Write(await email.RenderAsync(Mailocomotive.Render.RenderType.STRING));
            Console.WriteLine("");
            Console.WriteLine("Sending email to mailtrap");
            email
                .From("Sender", "sender@mail.com")
                .To("Demo", "demo@mail.com")
                .Subject("Demo");
            await email.SendAsync();
            Console.WriteLine("Sent!");
            Console.ReadLine();
        }
    }
}
