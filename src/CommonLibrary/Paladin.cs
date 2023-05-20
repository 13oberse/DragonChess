using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Paladin Movement</para>
///     <para>Level 2:  -can move and capture as a chess king+knight</para>
///     <para>Levels 1 &amp; 3: -can move and capture like a chess king</para>
///     <para>Any Level:  -can move to the other levels using a knight-like move:</para>
///     <para>-one level up or down followed by two steps orthogonally</para>
///     <para>-two levels up or down followed by one step orthogonally</para>
/// </summary>
public class Paladin : ChessPiece
{
    public Paladin(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (Immobile)
        {
            return moves;
        }

        return moves;
    }
}