using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Basilisk Movement</para>
///     <para>Level 1: -can move and capture one step diagonally forward or straight forward</para>
///     <para>-can move one step straight backward</para>
///     <para>Note: automatically freezes (immobilizes) an enemy piece on the square directly</para>
///     <para>above on level 2, whether the Basilisk moves to the space below or the enemy</para>
///     <para>moves to the space above, and until the Basilisk moves away or is captured</para>
/// </summary>
public class Basilisk : ChessPiece
{
    public Basilisk(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 0;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Position.Z != 0 || IsImmobilized(board))
        {
            return moves;
        }

        /* Index the Y-line in front of the piece */
        var nextYLine = Position.Y + (Owner == PlayerColor.White ? 1 : -1);
        if (nextYLine >= 0 && nextYLine < 8)
        {
            /* Can move or capture one step straight or diagonally forward */
            CheckMove(board, moves, Position.X - 1, nextYLine, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, nextYLine, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, nextYLine, 0, MoveType.MoveCapture);
        }

        /* Can move one step straight backwards */
        /* Index the Y-line behind the piece */
        var lastYLine = Position.Y + (Owner == PlayerColor.White ? -1 : 1);
        CheckMove(board, moves, Position.X, lastYLine, 0, MoveType.MoveOnly);

        return moves;
    }
}