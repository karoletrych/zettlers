namespace zettlers
{
    public class Request
    {
        public IPlayerCommand PlayerCommand { get; set; }
        public int LockstepTurnId { get; set; }
    }
}
