using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Thief Movement</para>
///     <para>Level 2: -can move and capture like a chess bishop</para>
/// </summary>
public class Thief : ChessPiece
{
    public Thief(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 12;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Position.Z != 1 || IsImmobilized(board))
        {
            return moves;
        }

        /* Can move and capture as a chess bishop */
        moves.AddRange(GetBishopMoves(board));

        return moves;
    }
}
