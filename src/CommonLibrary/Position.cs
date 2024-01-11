using System;
using System.Drawing;

namespace DragonChess.CommonLibrary;

public class Position
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public Position(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public void CopyPos(Position pos)
    {
        X = pos.X;
        Y = pos.Y;
        Z = pos.Z;
    }

    public void NewPos(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override bool Equals(Object? obj)
    {
        //Check for null and compare run-time types.
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Position p = (Position)obj;
            return X == p.X && Y == p.Y && Z == p.Z;
        }
    }

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}