using System;
using TME.Default.Interfaces;
using TME.Scenario.Default.Enums;

namespace TME.Scenario.Default.Base
{
    public struct Loc : IEquatable<Loc>, IMappable
    {
        public static readonly Loc Zero = new Loc(0,0);
        public int X;
        public int Y;

        // Table to work out directions
        private static readonly Loc[] DirectionLookTable = {
            new Loc(0,-1),       // DR_NORTH,
            new Loc(1,-1),       // DR_NORTHEAST,
            new Loc(1,0),        // DR_EAST,
            new Loc(1,1),        // DR_SOUTHEAST,
            new Loc(0,1),        // DR_SOUTH,
            new Loc(-1,1),       // DR_SOUTHWEST,
            new Loc(-1,0),       // DR_WEST,
            new Loc(-1,-1)       // DR_NORTHWEST,
        };

        public Loc Location { get => this; set { } }

        public Loc( Int32 x, Int32 y )
        {
            this.X = x;
            this.Y = y;
        }

        public Loc(Loc loc)
        {
            this.X = loc.X;
            this.Y = loc.Y;
        }

        public void Add( Loc loc )
        {
            this.X += loc.X;
            this.Y += loc.Y;
        }

        public void Subtract(Loc loc)
        {
            this.X -= loc.X;
            this.Y -= loc.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals( (Loc) obj  );
        }

        public bool Equals(Loc other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        public static bool operator == (Loc lhs, Loc rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Loc lhs, Loc rhs)
        {
            return lhs.Equals(rhs) == false ;
        }

        public static Loc operator + (Loc lhs, Loc rhs)
        {
            return new Loc(lhs.X + rhs.X, lhs.Y + rhs.Y);
        }

        public static Loc operator - (Loc lhs, Loc rhs)
        {
            return new Loc(lhs.X - rhs.X, lhs.Y - rhs.Y);
        }

        public void Add( Direction direction )
        {
            Add(DirectionLookTable[(int)direction]);
        }

        public void Subtract(Direction direction)
        {
            Subtract(DirectionLookTable[(int)direction]);
        }

        public static Loc operator +(Loc lhs, Direction direction)
        {
            var temp = new Loc(lhs);
            temp.Add(direction);
            return temp;
        }

        public static Loc operator -(Loc lhs, Direction direction)
        {
            var temp = new Loc(lhs);
            temp.Subtract(direction);
            return temp;
        }

        public Direction DirectionBetweenLocation( Loc location )
        {
            var delta = this - location;
            
            if (delta.X != 0) delta.X = Math.Sign(delta.X);
            if (delta.Y != 0) delta.Y = Math.Sign(delta.Y);

            var index = Array.IndexOf( DirectionLookTable, delta );
            return index == -1
                ? Direction.None
                : (Direction) index;
            
            // for (var ii = 0; ii < DirectionLookTable.GetUpperBound(0); ii++ ) {
            //     if (DirectionLookTable[ii].X == dx && DirectionLookTable[ii].Y == dy)
            //         return (Direction)ii;
            // }
            // return Direction.None;
        }


        public override string ToString() => $"[{this.X},{this.Y}]";
    
    }
}
