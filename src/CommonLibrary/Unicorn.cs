using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Unicorn Movement:</para>
///     <para>Level 2: -can move and capture like a chess knight</para>
/// </summary>
public class Unicorn : ChessPiece
{
    public Unicorn(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile || Position.Z != 1)
        {
            return moves; // Immobile or illegal position
        }

        /* Can move/capture like a chess knight */
        CheckMove(board, moves, Position.X - 1, Position.Y - 2, 1);
        CheckMove(board, moves, Position.X - 2, Position.Y - 1, 1);
        CheckMove(board, moves, Position.X - 1, Position.Y + 2, 1);
        CheckMove(board, moves, Position.X - 2, Position.Y + 1, 1);
        CheckMove(board, moves, Position.X + 1, Position.Y - 2, 1);
        CheckMove(board, moves, Position.X + 2, Position.Y - 1, 1);
        CheckMove(board, moves, Position.X + 1, Position.Y + 2, 1);
        CheckMove(board, moves, Position.X + 2, Position.Y + 1, 1);

        return moves;
    }
}