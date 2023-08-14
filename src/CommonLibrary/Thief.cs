using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Thief Movement</para>
///     <para>Level 2: -can move and capture like a chess bishop</para>
/// </summary>
public class Thief : ChessPiece
{
    public Thief(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        /* Check for mobility/valid position */
        if (Immobile || Position.Z != 1)
        {
            return moves;
        }

        /* Can move and capture as a chess bishop */
        /* Loop through all valid distances for each direction */
        for (var i = 1; i < 8; i++) // -x, -y direction
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X - i, Position.Y - i, 1, MoveType.MoveCapture))
                break;
        }

        for (var i = 1; i < 8; i++) // -x, +y direction
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X - i, Position.Y + i, 1, MoveType.MoveCapture))
                break;
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X + i, Position.Y - i, 1, MoveType.MoveCapture))
                break;
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X + i, Position.Y + i, 1, MoveType.MoveCapture))
                break;
        }

        return moves;
    }
}