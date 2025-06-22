
public interface IButtonReceiver<TEnum>
{
    public void InitializeButtonActions();
    public void StartButtonRelaysListening();
    public void StopButtonRelaysListening();
}
