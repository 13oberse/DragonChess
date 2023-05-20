using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Thief Movement</para>
///     <para>Level 2: -can move and capture like a chess bishop</para>
/// </summary>
public class Thief : ChessPiece
{
    public Thief(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    //TODO
    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        return moves;
    }
}