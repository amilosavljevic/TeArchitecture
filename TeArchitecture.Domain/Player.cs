using System;

namespace TeArchitecture.Domain
{
    public readonly struct PlayerId : IEquatable<PlayerId>
    {
        public readonly long Id;

        public PlayerId(long id) => Id = id;

        public override bool Equals(object obj) => obj is PlayerId other && Equals(other);
        public bool Equals(PlayerId other) => Id == other.Id;
        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator == (PlayerId p1, PlayerId p2) => p1.Equals(p2);
        public static bool operator != (PlayerId p1, PlayerId p2) => !p1.Equals(p2);
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
