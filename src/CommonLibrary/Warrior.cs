using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Warrior Movement:</para>
///     <para>Level 2: -can move and capture like a chess pawn but without the initial two-step option</para>
///     <para>Note: promotes to Hero when reaching the furthest rank</para>
/// </summary>
public class Warrior : ChessPiece
{
    public Warrior(bool white, int x, int y, int z) : base(white, x, y, z)
    {
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
            /* Can move like a chess pawn (but without the initial 2-step option) */
            /* Calculate the row it can move to */
            var nextYLine = Position.Y + (Owner ? 1 : -1);
            if (nextYLine is < 0 or >= 8)
            {
                return moves; // Illegal position/should've been promoted
            }

            /* Can move (but not capture) directly ahead */
            if (board[Position.X, nextYLine, 1] == null)
            {
                moves.Add(new Position(Position.X, nextYLine, 1));
            }

            /* Can capture one step diagonally forward */
            var positionToCheck = board[Position.X - 1, nextYLine, 1];
            if (Position.X - 1 >= 0 && positionToCheck != null && positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, nextYLine, 1));
            }

            positionToCheck = board[Position.X + 1, nextYLine, 1];
            if (Position.X + 1 < 12 && positionToCheck != null && positionToCheck.Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, nextYLine, 1));
            }
        }

        /* Invalid position */
        return moves;
    }
}