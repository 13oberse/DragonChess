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
    public Cleric(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 1;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (IsImmobilized(board))
        {
            return moves;
        }

        /* Can move/capture like a chess king */
        moves.AddRange(GetKingMoves(board));

        /* Can move directly up/down one square */
        CheckMove(board, moves, Position.X, Position.Y, Position.Z + 1, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X, Position.Y, Position.Z - 1, MoveType.MoveCapture);

        return moves;
    }
}
