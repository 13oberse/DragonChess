using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>King Movement</para>
///     <para>Level 2:  -can move and capture like a chess king</para>
///     <para>-can move and capture to the square directly below on level 1 or directly above on level 3</para>
///     <para>Levels 1 & 3: -can move to (only) the same square on level 2 the King previously left</para>
/// </summary>
public class King : ChessPiece
{
    public King(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 6;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        if (Position.Z == 1)
        {
            /* Can move/capture like a chess king */
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1, MoveType.MoveCapture);

            CheckMove(board, moves, Position.X, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y - 1, 1, MoveType.MoveCapture);

            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1, MoveType.MoveCapture);

            /* Can move/capture directly up or down */
            CheckMove(board, moves, Position.X, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y, 2, MoveType.MoveCapture);
        }
        else /* Z is not 1 (so 0 or 2) */
        {
            /* Can move directly up/down to surface (z=1) */
            CheckMove(board, moves, Position.X, Position.Y, 1, MoveType.MoveOnly);
        }

        return moves;
    }
}