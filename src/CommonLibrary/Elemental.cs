using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Elemental Movement</para>
///     <para>Level 1: -can move and capture one or two steps orthogonally</para>
///     <para>-can move one step diagonally</para>
///     <para>-can capture in the following pattern: one step orthogonally followed by the square directly above on level 2</para>
///     <para>Level 2: -can move and capture in the following pattern: the square directly below on level 1 followed by one step orthogonally</para>
/// </summary>
public class Elemental : ChessPiece
{
    public Elemental(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Immobile || Position.Z == 2)
        {
            return moves;
        }

        if (Position.Z == 0)
        {
            /* Can move/capture one or two steps orthogonally */
            /* NOTE: Gygax was unclear, I'm assuming it can jump over an intermediate piece here */
            CheckMove(board, moves, Position.X - 2, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y - 2, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y - 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y + 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y + 2, 0, MoveType.MoveCapture);

            /* Can move one step diagonally */
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 0, MoveType.MoveOnly);

            /* Can capture the surface (z=1) squares orthogonal to the square directly above (an upwards diagonal) */
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1, MoveType.CaptureOnly);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1, MoveType.CaptureOnly);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1, MoveType.CaptureOnly);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1, MoveType.CaptureOnly);
        }

        if (Position.Z == 1)
        {
            /* can move/capture squares orthogonally adjacent to the square directly below underground (z=0) */
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 0, MoveType.MoveCapture);
        }

        return moves;
    }
}