namespace TeArchitecture.Domain
{    
    public readonly struct PlayerId
    {
        public readonly long Id;

        public PlayerId(long id) => Id = id;
    }

    public readonly struct Moral
    {
        public readonly int Value;

        public Moral(int value) => Value = value;
    }

    public readonly struct Condition
    {
        public readonly int Value;

        public Condition(int value) => Value = value;
    }

    public interface IPlayerData
    {
        PlayerId Id { get; }
        string Name { get; }
        int Age { get; }
        Moral Moral { get; }
        Condition Condition { get; }
    }

    public class PlayerData : IPlayerData
    {
        public PlayerId Id { get; set; }
        public string Name { get; set; }

        public Condition Condition { get; set; }
        public Moral Moral { get; set; }
        public int Age { get; set; }
    }
}
