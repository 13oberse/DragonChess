using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Hero Movement</para>
///     <para>Level 2: -can move and capture one or two unblockable steps diagonally</para>
///     <para>-can move and capture one step triagonally to levels 1 or 3</para>
///     <para>Levels 1 & 3: -can move and capture back to the square on level 2 the Hero previously left</para>
/// </summary>
public class Hero : ChessPiece
{
    public Hero(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 5;
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
            /* Can move/capture one or two unblockable steps diagonally */
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 1, MoveType.MoveCapture);

            CheckMove(board, moves, Position.X + 2, Position.Y + 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 2, Position.Y + 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 2, Position.Y - 2, 1, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 2, Position.Y - 2, 1, MoveType.MoveCapture);

            /* Can move/capture one step triagonally to levels 1 or 3 */
            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 0, MoveType.MoveCapture);

            CheckMove(board, moves, Position.X - 1, Position.Y - 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X - 1, Position.Y + 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y - 1, 2, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X + 1, Position.Y + 1, 2, MoveType.MoveCapture);
        }
        else /* Z is not 1 (so 0 or 2) */
        {
            /* Can move/capture back to the square on level 2 the Hero previously left */
            CheckMove(board, moves, LastPosition.X, LastPosition.Y, LastPosition.Z, MoveType.MoveCapture);
        }

        return moves;
    }
}