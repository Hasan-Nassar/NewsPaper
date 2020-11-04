namespace Common.Events
{
    public interface IAuthenticateEvent: IEvent
    { 
        string UserId { get; set; }
    }
}