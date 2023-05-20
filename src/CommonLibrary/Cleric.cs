using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Cleric Movement</para>
///     <para>Any Level: -can move and capture like a chess king</para>
///     <para>-can move and capture to the square directly above or</para>
///     <para>directly below on an adjacent level</para>
/// </summary>
public class Cleric : ChessPiece
{
    public Cleric(bool white, int x, int y, int z) : base(white, x, y, z)
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