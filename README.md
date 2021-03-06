# Mailocomotive
![Nuget](https://img.shields.io/nuget/v/Mailocomotive)
## Usage
1. Download the package on [NuGet](https://www.nuget.org/packages/Mailocomotive)
2. Create a `Provider` instance [(see Providers)](#providers)
3. Use the Fluent API to configure Mailocomotive
```csharp
Api.Configuration()
    .UseProjectRoot(path)//optional
    .UseProvider(provider);
```
4. Create an email by creating a class that implements `Mailocomotive.Email<TViewModel>`:
```csharp
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
```
5. Configure the email headers and send the mail
```csharp
await email
    .From("Sender", "sender@example.com")
    .To("Demo", "demo@example.com")
    .Subject("Demo")
    .SendAsync();
```
## Providers
Currently the package only supports SMTP providers.
### Single mail provider
If you use only one SMTP provider use [`SmtpMailProvder`](https://github.com/Setarit/Mailocomotive/blob/main/Mailocomotive/Setting/Single/SmtpMailProvider.cs)
### Multiple providers
It's possible that you have multiple providers.
You can choose to rotate between the available providers, or let the package pick one at random.
Use the [`SmtpMailProviderCollection`](https://github.com/Setarit/Mailocomotive/blob/main/Mailocomotive/Setting/Multiple/Smtp/SmtpMailProviderCollection.cs).
The `Collection` property contains all the `SmtpMailProvder`s to use.
The `Strategy` property contains the how the package should handle the collection.

| Strategy | Description                                                                                                                                                                                                                |
|----------|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `ROTATE` | Tries to send the mail with the first provider. If that fail it tries the second, if the second fails then it tries the third and so on.  Throws the last exception if all failed with exception                           |
| `RANDOM` | Uses a random provider from the collection to send.  If the provider fails, another provider will be selected at random. When all the providers in the collection fail, it will throw the exception from the last provider |

## Examples
You can find examples in the `Examples` folder