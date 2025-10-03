using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Mage Movement</para>
///     <para>Level 2:  -can move and capture like a chess queen</para>
///     <para>Levels 1 & 3: -can move one step orthogonally</para>
///     <para>Any Level: -can move and capture one or two steps (blockable) directly above or directly below to one of the other levels</para>
/// </summary>
public class Mage : ChessPiece
{
    public Mage(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 7;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (IsImmobilized(board))
        {
            return moves;
        }
        /* Can move/capture one/two steps (blockable) directly up or down to other levels */
        if (Position.Z == 1 || CheckMove(board, moves, Position.X, Position.Y, 1, MoveType.MoveCapture))
        {
            /* Either Mage is on surface (z=1) or that spot is empty */
            /* Note: calling CheckMove on Mage's position won't add that position to moves, as intended */
            CheckMove(board, moves, Position.X, Position.Y, 0, MoveType.MoveCapture);
            CheckMove(board, moves, Position.X, Position.Y, 2, MoveType.MoveCapture);
        }

        if (Position.Z == 1)
        {
            /* Can move/capture like a chess queen (which is just rook and bishop) */
            moves.AddRange(GetRookMoves(board));
            moves.AddRange(GetBishopMoves(board)); /* Shouldn't overlap with rook moves, so no need to exclude here */
        }
        else /* z is not 1 (so 0 or 2) */
        {
            /* Can move one step orthogonally */
            CheckMove(board, moves, Position.X + 1, Position.Y,     Position.Z, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X - 1, Position.Y,     Position.Z, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y + 1, Position.Z, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y - 1, Position.Z, MoveType.MoveOnly);
        }

        return moves;
    }
}
