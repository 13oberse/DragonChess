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
    public Basilisk(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (Immobile)
        {
            return moves;
        }

        return moves;
    }

    public override void MoveTo(ChessPiece?[,,] board, int x, int y, int z)
    {
        // TODO: immobilize/remobilize pieces
        LastPosition.CopyPos(Position);
        Position.NewPos(x, y, z);

        // Update the board (note: this currently loses references to any captured pieces)
        board[Position.X, Position.Y, Position.Z] = this;
        board[LastPosition.X, LastPosition.Y, LastPosition.Z] = null;
    }
}