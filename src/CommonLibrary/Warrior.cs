using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Warrior Movement:</para>
///     <para>Level 2: -can move and capture like a chess pawn but without the initial two-step option</para>
///     <para>Note: promotes to Hero when reaching the furthest rank</para>
/// </summary>
public class Warrior : ChessPiece
{
    public Warrior(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
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
            var nextYLine = Position.Y + (Owner == PlayerColor.White ? 1 : -1);

            /* Can move (but not capture) directly ahead */
            CheckMove(board, moves, Position.X, nextYLine, 1, MoveType.MoveOnly);

            /* Can capture one step diagonally forward */
            CheckMove(board, moves, Position.X - 1, nextYLine, 1, MoveType.CaptureOnly);
            CheckMove(board, moves, Position.X + 1, nextYLine, 1, MoveType.CaptureOnly);
        }

        /* Invalid position */
        return moves;
    }

    public override void MoveTo(ChessPiece?[,,] board, int x, int y, int z)
    {
        /* Check if this Warrior should be promoted (if it got to the back row) */
        if (Owner == PlayerColor.White ? (y==7):(y==0))
        {
            /* Promote self to Hero (Hero.Position set by constructor) */
            ChessPiece HeroPromotion = new Hero(Owner, x, y, z);

            /* Update Hero's LastPosition */
            HeroPromotion.LastPosition.CopyPos(Position);

            /* Update the board (note: this makes a new reference to Hero, and abandons this Warrior instance) */
            board[x, y, z] = HeroPromotion;
            board[Position.X, Position.Y, Position.Z] = null;
        }
        else
        {
            // Update the local current and last positions
            LastPosition.CopyPos(Position);
            Position.NewPos(x, y, z);

            // Update the board (note: this currently loses references to any captured pieces)
            board[Position.X, Position.Y, Position.Z] = this;
            board[LastPosition.X, LastPosition.Y, LastPosition.Z] = null;
        }
    }
}