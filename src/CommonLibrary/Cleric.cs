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

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        /* Can move/capture like a chess king */
        CheckMove(board, moves, Position.X - 1, Position.Y - 1, Position.Z, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X - 1, Position.Y, Position.Z, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X - 1, Position.Y + 1, Position.Z, MoveType.MoveCapture);

        CheckMove(board, moves, Position.X, Position.Y - 1, Position.Z, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X, Position.Y + 1, Position.Z, MoveType.MoveCapture);

        CheckMove(board, moves, Position.X + 1, Position.Y - 1, Position.Z, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X + 1, Position.Y, Position.Z, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X + 1, Position.Y + 1, Position.Z, MoveType.MoveCapture);

        /* Can move directly up/down one square */
        CheckMove(board, moves, Position.X, Position.Y, Position.Z + 1, MoveType.MoveCapture);
        CheckMove(board, moves, Position.X, Position.Y, Position.Z - 1, MoveType.MoveCapture);

        return moves;
    }
}