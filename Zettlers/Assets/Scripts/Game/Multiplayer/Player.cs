using System;

namespace zettlers
{
    public struct Player : IEquatable<Player>
    {
        public Player(string name) => Name = name;
        public string Name { get; }

        public override bool Equals(Object other) => other is Player && ((Player)other).Name == this.Name;
        public bool Equals(Player other) => this.Name == other.Name;
        public override int GetHashCode() => Name.GetHashCode();
        public static bool operator ==(Player x, Player y) => x.Name == y.Name;
        public static bool operator !=(Player x, Player y) => x.Name != y.Name;
    }
}
