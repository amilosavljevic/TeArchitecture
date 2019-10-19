namespace TeArchitecture.Shared
{
    public readonly struct PlayerId
    {
        public readonly int Id;

        public PlayerId(int id) => Id = id;
    }

    public class PlayerInfo
    {
        public readonly PlayerId Id;
    }
}
