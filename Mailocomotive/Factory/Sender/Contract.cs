namespace Mailocomotive.Factory.Sender
{
    public interface Contract<TViewModel>
    {
        Mailocomotive.Sender.Sender<TViewModel> Create();
    }
}
