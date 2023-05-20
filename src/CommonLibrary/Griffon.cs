using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Griffon movement:</para>
///     <para>Level 3: -can move and capture like a knight, except it's a (2,3) jump</para>
///     <para>-can move and capture one step triagonally to level 2</para>
///     <para>Level 2: -can move and capture one step diagonally</para>
///     <para>-can move and capture one step triagonally to level 3</para>
/// </summary>
public class Griffon : ChessPiece
{
    public Griffon(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        if (Position.Z == 2)
        {
            /* Can move and capture like a knight, but in a (2,3) jump */
            /* Screw it, check them manually */
            CheckMove(board, moves, Position.X - 2, Position.Y - 3, 2);
            CheckMove(board, moves, Position.X - 3, Position.Y - 2, 2);
            CheckMove(board, moves, Position.X - 2, Position.Y + 3, 2);
            CheckMove(board, moves, Position.X - 3, Position.Y + 2, 2);
            CheckMove(board, moves, Position.X + 2, Position.Y - 3, 2);
            CheckMove(board, moves, Position.X + 2, Position.Y - 3, 2);
            CheckMove(board, moves, Position.X + 2, Position.Y + 3, 2);
            CheckMove(board, moves, Position.X + 3, Position.Y + 2, 2);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1);
        }

        if (Position.Z == 1)
        {
            /* Can move and capture one step diagonally or */
            /* move and capture one step triagonally to the top level */
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 2);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 2);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 2);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 2);
        }

        /* Illegal position */
        return moves;
    }
}