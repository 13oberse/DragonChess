using System.Collections.Generic;
using System.Linq;

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
    public Dragon(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 10;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Position.Z != 2 || IsImmobilized(board))
        {
            return moves; // Immobile or illegal position
        }

        /* Can move and capture as a chess bishop */
        moves.AddRange(GetBishopMoves(board));

        /* Can also move and capture as a chess king (don't add duplicate positions to list) */
        moves.AddRange(GetKingMoves(board).Except(moves));

        return moves;
    }

    public override List<Position> RemoteCaptures(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (IsImmobilized(board))
        {
            return moves;
        }

        /* Can remotely capture any piece in the 3x3 below it */
        if (Position.Z == 2)
        {
            /* Admittedly redundant check to ensure this is a valid space */
            if (Position.X >= 0 && Position.X < 12 &&
                Position.Y >= 0 && Position.Y < 8)
            {
                /* Check the square below this piece and the four orthogonally adjacent squares */
                var positionsToCheck = new List<ChessPiece?>();
                positionsToCheck.Add(board[Position.X, Position.Y, 1]);
                if (Position.X+1 < 12) {
                    positionsToCheck.Add(board[Position.X+1, Position.Y, 1]);
                }
                if (Position.X-1 >= 0) {
                    positionsToCheck.Add(board[Position.X-1, Position.Y, 1]);
                }
                if (Position.Y+1 < 8) {
                    positionsToCheck.Add(board[Position.X, Position.Y+1, 1]);
                }
                if (Position.Y-1 >= 0) {
                    positionsToCheck.Add(board[Position.X, Position.Y-1, 1]);
                }

                /* Now check all those cells */
                positionsToCheck.ForEach(delegate(ChessPiece? piece)
                {
                    if (piece != null && piece.Owner != Owner)
                    {
                        moves.Add(new Position(piece.Position.X, piece.Position.Y, piece.Position.Z));
                    }
                });
            }
        }

        /* Illegal position */
        return moves;
    }
}
