using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Mage Movement</para>
///     <para>Level 2:  -can move and capture like a chess queen</para>
///     <para>Levels 1 &amp; 3: -can move one step orthogonally</para>
///     <para>Any Level: -can move and capture one or two steps (blockable) directly above or directly below to one of the other levels</para>
/// </summary>
public class Mage : ChessPiece
{
    public Mage(bool white, int x, int y, int z) : base(white, x, y, z)
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
}