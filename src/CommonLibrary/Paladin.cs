using System.Collections.Generic;
using System.Linq;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Paladin Movement</para>
///     <para>Level 2:  -can move and capture as a chess king+knight</para>
///     <para>Levels 1 & 3: -can move and capture like a chess king</para>
///     <para>Any Level:  -can move to the other levels using a knight-like move:</para>
///     <para>-one level up or down followed by two steps orthogonally</para>
///     <para>-two levels up or down followed by one step orthogonally</para>
/// </summary>
public class Paladin : ChessPiece
{
    public Paladin(PlayerColor color, int x, int y, int z) : base(color, x, y, z)
    {
        ImgID = 9;
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (IsImmobilized(board))
        {
            return moves;
        }

        /* Can move/capture like a chess king */
        moves.AddRange(GetKingMoves(board));

        if (Position.Z == 1)
        {
            /* Can move/capture like a chess knight */
            moves.AddRange(GetJumpMoves(board, 1, 2).Except(moves));

            /* Move in a vertical knight patttern from surface */
            CheckMove(board, moves, Position.X - 2, Position.Y,     0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y,     0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y - 2, 0, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y + 2, 0, MoveType.MoveOnly);

            CheckMove(board, moves, Position.X - 2, Position.Y,     2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y,     2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y - 2, 2, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y + 2, 2, MoveType.MoveOnly);
        }
        else /* Z is not 1 (so 0 or 2) */
        {
            /* If not on surface (z=1), can move in vertical knight pattern to surface */
            CheckMove(board, moves, Position.X - 2, Position.Y,     1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 2, Position.Y,     1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y - 2, 1, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y + 2, 1, MoveType.MoveOnly);

            /* Can move in vertical knight pattern, only cases not covered yet are jumping between z=0 and 2 */
            /* Get Z position opposite the Paladin's current Z */
            var oppositeZ = (Position.Z == 0) ? 2 : 0;

            /* Can move in vertical knight pattern between underground and sky */
            CheckMove(board, moves, Position.X - 1, Position.Y,     oppositeZ, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X + 1, Position.Y,     oppositeZ, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y - 1, oppositeZ, MoveType.MoveOnly);
            CheckMove(board, moves, Position.X,     Position.Y + 1, oppositeZ, MoveType.MoveOnly);
        }

        return moves;
    }
}
