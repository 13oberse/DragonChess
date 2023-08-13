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

            /* Can move (but not capture) directly ahead */
            CheckMove(board, moves, Position.X, nextYLine, 1, MoveType.MoveOnly);

            /* Can capture one step diagonally forward */
            CheckMove(board, moves, Position.X - 1, nextYLine, 1, MoveType.CaptureOnly);
            CheckMove(board, moves, Position.X + 1, nextYLine, 1, MoveType.CaptureOnly);
        }

        /* Invalid position */
        return moves;
    }
}