using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Oliphant Movement:</para>
///     <para>Level 2: -can move and capture like a chess rook</para>
/// </summary>
public class Oliphant : ChessPiece
{
    public Oliphant(bool white, int x, int y, int z) : base(white, x, y, z)
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

        /* Can move and capture like a chess rook */
        /* Loop through each direction until a piece (or the edge of the board) blocks movement */
        /* Negative horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X - i, Position.Y, 1, MoveType.MoveCapture))
                break;
        }

        /* Positive horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X + i, Position.Y, 1, MoveType.MoveCapture))
                break;
        }

        /* Negative vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X, Position.Y - i, 1, MoveType.MoveCapture))
                break;
        }

        /* Positive vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If there's a piece there, break out of loop */
            if (!CheckMove(board, moves, Position.X, Position.Y + i, 1, MoveType.MoveCapture))
                break;
        }

        return moves;
    }
}