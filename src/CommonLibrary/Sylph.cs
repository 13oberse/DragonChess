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
    public Sylph(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 11;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (IsImmobilized(board))
        {
            return moves;
        }

        if (Position.Z == 1)
        {
            /* Can move up if space above is unoccupied */
            CheckMove(board, moves, Position.X, Position.Y, 2, MoveType.MoveOnly);

            /* Can move to any of the starting sylph positions */
            var sylphStartY = Owner == PlayerColor.White ? 1 : 6;
            for (var x = 0; x < 12; x += 2)
            {
                CheckMove(board, moves, x, sylphStartY, 2, MoveType.MoveOnly);
            }
        }
        else if (Position.Z == 2)
        {
            /* Can capture directly below */
            CheckMove(board, moves, Position.X, Position.Y, 1, MoveType.CaptureOnly);

            /* Index the Y-line in front of the piece */
            var nextYLine = Position.Y + (Owner == PlayerColor.White ? 1 : -1);

            /* Can capture directly forward */
            CheckMove(board, moves, Position.X, nextYLine, 2, MoveType.CaptureOnly);

            /* Can move diagonally foward */
            CheckMove(board, moves, Position.X + 1, nextYLine, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X - 1, nextYLine, 2, MoveType.MoveOnly);
        }

        /* Illegal position */
        return moves;
    }
}