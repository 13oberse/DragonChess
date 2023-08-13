using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Paladin Movement</para>
///     <para>Level 2:  -can move and capture as a chess king+knight</para>
///     <para>Levels 1 & 3: -can move and capture like a chess king</para>
///     <para>Any Level:  -can move to the other levels using a knight-like move:</para>
///     <para>-one level up or down followed by two steps orthogonally</para>
///     <para>-two levels up or down followed by one step orthogonally</para>
/// </summary>
public class Paladin : ChessPiece
{
    public Paladin(bool white, int x, int y, int z) : base(white, x, y, z)
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

        if (Position.Z == 1)
        {
            /* Can move/capture like a chess knight */
            CheckMove(board, moves, Position.X - 2, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 2, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 2, 1, MoveType.MoveCapture);

            /* Move  */
        }

        return moves;
    }
}