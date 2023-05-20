using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Oliphant Movement:</para>
///     <para>Level 2: -can move and capture like a chess rook</para>
/// </summary>
public class Oliphant : ChessPiece
{
    public Oliphant(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile || Position.Z != 1)
        {
            return moves; // Immobilized or illegal position
        }

        /* Can move and capture like a chess rook */
        /* Loop through each direction until a piece (or the edge of the board) blocks movement */
        /* Negative horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X - i, Position.Y, 1];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y, 1));
        }

        /* Positive horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X + i, Position.Y, 1];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y, 1));
        }

        /* Negative vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X, Position.Y - i, 1];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X, Position.Y - i, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X, Position.Y - i, 1));
        }

        /* Positive vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X, Position.Y + i, 1];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X, Position.Y + i, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X, Position.Y + i, 1));
        }

        return moves;
    }
}