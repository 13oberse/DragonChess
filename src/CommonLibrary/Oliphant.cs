using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Oliphant Movement:</para>
///     <para>Level 2: -can move and capture like a chess rook</para>
/// </summary>
public class Oliphant : ChessPiece
{
    public Oliphant(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 8;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Position.Z != 1 || IsImmobilized(board))
        {
            return moves;
        }

        /* Can move and capture like a chess rook */
        moves.AddRange(GetRookMoves(board));

        return moves;
    }
}
