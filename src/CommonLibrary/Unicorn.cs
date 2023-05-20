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

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (Immobile || Position.Z != 1)
        {
            return moves; // Immobile or illegal position
        }

        /* Can move/capture like a chess knight */
        if (Position.X - 1 >= 0 && Position.Y - 2 >= 0)
        {
            if (board[Position.X - 1, Position.Y - 2, 1] == null ||
                board[Position.X - 1, Position.Y - 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y - 2, 1));
            }
        }

        if (Position.X - 2 >= 0 && Position.Y - 1 >= 0)
        {
            if (board[Position.X - 2, Position.Y - 1, 1] == null ||
                board[Position.X - 2, Position.Y - 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 2, Position.Y - 1, 1));
            }
        }

        if (Position.X - 1 >= 0 && Position.Y + 2 < 8)
        {
            if (board[Position.X - 1, Position.Y + 2, 1] == null ||
                board[Position.X - 1, Position.Y + 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y + 2, 1));
            }
        }

        if (Position.X - 2 >= 0 && Position.Y + 1 < 8)
        {
            if (board[Position.X - 2, Position.Y + 1, 1] == null ||
                board[Position.X - 2, Position.Y + 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 2, Position.Y + 1, 1));
            }
        }

        if (Position.X + 1 < 12 && Position.Y - 2 >= 0)
        {
            if (board[Position.X + 1, Position.Y - 2, 1] == null ||
                board[Position.X + 1, Position.Y - 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y - 2, 1));
            }
        }

        if (Position.X + 2 < 12 && Position.Y - 1 >= 0)
        {
            if (board[Position.X + 2, Position.Y - 1, 1] == null ||
                board[Position.X + 2, Position.Y - 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 2, Position.Y - 1, 1));
            }
        }

        if (Position.X + 1 < 12 && Position.Y + 2 < 8)
        {
            if (board[Position.X + 1, Position.Y + 2, 1] == null ||
                board[Position.X + 1, Position.Y + 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y + 2, 1));
            }
        }

        if (Position.X + 2 < 12 && Position.Y + 1 < 8)
        {
            if (board[Position.X + 2, Position.Y + 1, 1] == null ||
                board[Position.X + 2, Position.Y + 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 2, Position.Y + 1, 1));
            }
        }

        return moves;
    }
}