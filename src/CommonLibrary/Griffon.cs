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
    public Griffon(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
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
            CheckMove(board, moves, Position.X - 2, Position.Y - 3, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 3, Position.Y - 2, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 2, Position.Y + 3, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 3, Position.Y + 2, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y - 3, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y - 3, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y + 3, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 3, Position.Y + 2, 2, MoveType.MoveCapture);
            /* Can move and capture one step triagonally */
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1, MoveType.MoveCapture);
        }

        if (Position.Z == 1)
        {
            /* Can move and capture one step diagonally or */
            /* move and capture one step triagonally to the top level */
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 2, MoveType.MoveCapture);
        }

        /* Illegal position */
        return moves;
    }
}