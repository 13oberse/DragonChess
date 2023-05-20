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
}