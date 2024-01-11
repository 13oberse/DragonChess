using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Dwarf Movement</para>
///     <para>Level 1: -can move one step straight forward or sideways, or capture one step diagonally forward</para>
///     <para>-can capture on the square directly above on level 2</para>
///     <para>Level 2: -can move one step straight forward or sideways, or capture one step diagonally forward</para>
///     <para>-can move to the square directly below on level 1</para>
/// </summary>
public class Dwarf : ChessPiece
{
    public Dwarf(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 2;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Position.Z == 2 || IsImmobilized(board))
        {
            return moves;
        }

        if (Position.Z == 0)
        {
            /* Can capture directly above on surface (z=1) */
            CheckMove(board, moves, Position.X, Position.Y, 1, MoveType.CaptureOnly);
        }

        if (Position.Z == 1)
        {
            /* Can move directly below to underground (z=0) */
            CheckMove(board, moves, Position.X, Position.Y, 0, MoveType.MoveOnly);
        }

        /* Can move one step sideways */
        CheckMove(board, moves, Position.X + 1, Position.Y, Position.Z, MoveType.MoveOnly);
        CheckMove(board, moves, Position.X - 1, Position.Y, Position.Z, MoveType.MoveOnly);

        /* Index the Y-line in front of the piece */
        var nextYLine = Position.Y + (Owner == PlayerColor.White ? 1 : -1);

        /* Can move one step directly forward */
        CheckMove(board, moves, Position.X, nextYLine, Position.Z, MoveType.MoveOnly);

        /* Can capture diagonally forward */
        CheckMove(board, moves, Position.X + 1, nextYLine, Position.Z, MoveType.CaptureOnly);
        CheckMove(board, moves, Position.X - 1, nextYLine, Position.Z, MoveType.CaptureOnly);

        return moves;
    }
}