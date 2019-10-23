﻿using System;

namespace TeArchitecture.Domain
{
    public readonly struct PlayerId : IEquatable<PlayerId>
    {
        private readonly long id;

        public PlayerId(long id) => this.id = id;

        public override bool Equals(object obj) => obj is PlayerId other && Equals(other);
        public bool Equals(PlayerId other) => id == other.id;
        public override int GetHashCode() => id.GetHashCode();

        public static bool operator == (PlayerId p1, PlayerId p2) => p1.Equals(p2);
        public static bool operator != (PlayerId p1, PlayerId p2) => !p1.Equals(p2);

        public static implicit operator long(PlayerId pId) => pId.id;
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
