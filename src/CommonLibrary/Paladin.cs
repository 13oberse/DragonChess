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
    public Paladin(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
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

            /* Move in a vertical knight patttern from surface */
            CheckMove(board, moves, Position.X - 2, Position.Y, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y - 2, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y + 2, 0, MoveType.MoveOnly);

            CheckMove(board, moves, Position.X - 2, Position.Y, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y - 2, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y + 2, 2, MoveType.MoveOnly);
        }
        else /* Z is not 1 (so 0 or 2) */
        {
            /* If not on surface (z=1), can move in vertical knight pattern to surface */
            CheckMove(board, moves, Position.X - 2, Position.Y, 1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y, 1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y - 2, 1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y + 2, 1, MoveType.MoveOnly);
        }

        /* Can move in vertical knight pattern, only cases not covered yet are jumping between z=0 and 2 */
        if (Position.Z == 0)
        {
            /* Can move in vertical knight pattern from underground to sky */
            CheckMove(board, moves, Position.X - 1, Position.Y, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 1, Position.Y, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y - 1, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y + 1, 2, MoveType.MoveOnly);
        }
        if (Position.Z == 2)
        {
            /* Can move in vertical knight pattern from sky to underground */
            CheckMove(board, moves, Position.X - 1, Position.Y, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 1, Position.Y, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y - 1, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X, Position.Y + 1, 0, MoveType.MoveOnly);
        }

        return moves;
    }
}