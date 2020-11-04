namespace Common.Events
{
    public class UserCreatedEvent : IAuthenticateEvent
    
    {
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
