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

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (Immobile)
        {
            return moves;
        }

        if (Position.Z == 1)
        {
            /* Can move like a chess pawn (but without the initial 2-step option) */
            /* Calculate the row it can move to */
            var nextYLine = Position.Y + (Owner ? 1 : -1);
            if (nextYLine < 0 || nextYLine >= 8)
            {
                return moves; // Illegal position/should've been promoted
            }

            /* Can move (but not capture) directly ahead */
            if (board[Position.X, nextYLine, 1] == null)
            {
                moves.Add(new Position(Position.X, nextYLine, 1));
            }

            /* Can capture one step diagonally forward */
            if (Position.X - 1 >= 0 && board[Position.X - 1, nextYLine, 1] != null)
            {
                moves.Add(new Position(Position.X - 1, nextYLine, 1));
            }

            if (Position.X + 1 < 12 && board[Position.X + 1, nextYLine, 1] != null)
            {
                moves.Add(new Position(Position.X + 1, nextYLine, 1));
            }
        }

        /* Invalid position */
        return moves;
    }
}