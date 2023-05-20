using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>King Movement</para>
///     <para>Level 2:  -can move and capture like a chess king</para>
///     <para>-can move and capture to the square directly below on level 1 or directly above on level 3</para>
///     <para>Levels 1 &amp; 3: -can move to (only) the same square on level 2 the King previously left</para>
/// </summary>
public class King : ChessPiece
{
    public King(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

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