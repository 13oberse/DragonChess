using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Dragon Movement:</para>
///     <para>Level 3: -can move and capture as a chess king+bishop</para>
///     <para>
///         -can capture remotely (without moving) on the square
///         directly below on level 2, or on any square orthogonally adjacent to that square
///     </para>
/// </summary>
public class Dragon : ChessPiece
{
    public Dragon(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile || Position.Z != 2)
        {
            return moves; // Immobile or illegal position
        }

        /* Can move and capture as a chess bishop */
        /* Loop through all valid distances for each direction */
        for (var i = 1; i < 8; i++) // -x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0 || Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X - i, Position.Y - i, 2];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y - i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y - i, 2));
        }

        for (var i = 1; i < 8; i++) // -x, +y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0 || Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X - i, Position.Y + i, 2];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y + i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y + i, 2));
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12 || Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X + i, Position.Y - i, 2];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y - i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y - i, 2));
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12 || Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            var blockingPiece = board[Position.X + i, Position.Y + i, 2];
            if (blockingPiece != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (blockingPiece.Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y + i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y + i, 2));
        }

        /* Can also move and capture as a chess king */
        /* Since diagonals are covered above, only orthogonal spaces need checked */
        if (Position.X - 1 >= 0)
        {
            var positionToCheck = board[Position.X - 1, Position.Y, 2];
            if (positionToCheck == null || positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y, 2));
            }
        }

        if (Position.X + 1 < 12)
        {
            var positionToCheck = board[Position.X + 1, Position.Y, 2];
            if (positionToCheck == null || positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y, 2));
            }
        }

        if (Position.Y - 1 >= 0)
        {
            var positionToCheck = board[Position.X, Position.Y - 1, 2];
            if (positionToCheck == null || positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y - 1, 2));
            }
        }

        if (Position.Y + 1 < 8)
        {
            var positionToCheck = board[Position.X, Position.Y + 1, 2];
            if (positionToCheck == null || positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y + 1, 2));
            }
        }

        return moves;
    }

    public override List<Position> RemoteCaptures(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        /* Can remotely capture any piece in the 3x3 below it */
        if (Position.Z == 2)
        {
            /* Loop through 3x3 area below this piece */
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    /* Ensure this square is on the board */
                    if (Position.X + i >= 0 && Position.Y + j >= 0 &&
                        Position.X + i < 12 && Position.Y + j < 8)
                        /* If there is an enemy piece here, it can be captured */
                    {
                        var positionToCheck = board[Position.X + i, Position.Y + j, 1];
                        if (positionToCheck != null && positionToCheck.Owner != Owner)
                        {
                            moves.Add(new Position(Position.X + i, Position.Y + i, 1));
                        }
                    }
                }
            }
        }

        /* Illegal position */
        return moves;
    }
}