using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Sylph movement:</para>
///     <para>Level 3: -can move one step diagonally forward, or capture one step straight forward</para>
///     <para>-can capture on the square directly below on level 2</para>
///     <para>Level 2: -can move to the square directly above on level 3,</para>
///     <para>or to one of the player's six Sylph starting squares</para>
/// </summary>
public class Sylph : ChessPiece
{
    public Sylph(bool white, int x, int y, int z) : base(white, x, y, z)
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
            /* Can move up if space above is unoccupied */
            if (board[Position.X, Position.Y, 2] == null)
            {
                moves.Add(new Position(Position.X, Position.Y, 2));
            }

            /* Can move to any of the starting sylph positions */
            var sylphStartY = Owner ? 1 : 6;
            for (var x = 0; x <= 10; x += 2)
            {
                if (board[x, sylphStartY, 2] == null)
                {
                    moves.Add(new Position(x, sylphStartY, 2));
                }
            }
        }
        else if (Position.Z == 2)
        {
            /* Can capture directly below */
            var positionBelow = board[Position.X, Position.Y, 1];
            if (positionBelow != null && positionBelow.Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y, 1));
            }

            /* Index the Y-line in front of the piece */
            var nextYLine = Position.Y + (Owner ? 1 : -1);
            if (nextYLine < 0 || nextYLine >= 8)
            {
                return moves; // Can't move forward
            }

            /* Can capture directly forward */
            var positionForward = board[Position.X, nextYLine, 2];
            if (positionForward != null && positionForward.Owner != Owner)
            {
                moves.Add(new Position(Position.X, nextYLine, 2));
            }

            /* Can move diagonally foward */
            if (Position.X < 12 && board[Position.X + 1, nextYLine, 2] == null)
            {
                moves.Add(new Position(Position.X + 1, nextYLine, 2));
            }

            if (Position.X > 0 && board[Position.X - 1, nextYLine, 2] == null)
            {
                moves.Add(new Position(Position.X - 1, nextYLine, 2));
            }
        }

        /* Illegal position */
        return moves;
    }
}