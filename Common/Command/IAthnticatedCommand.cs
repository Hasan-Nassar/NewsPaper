namespace Common.Commands
{
    public interface IAthnticatedCommand :ICommand
    {
        string UserId { get; set; }
    }
}