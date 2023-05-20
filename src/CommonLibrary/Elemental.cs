using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Elemental Movement</para>
///     <para>Level 1: -can move and capture one or two steps orthogonally</para>
///     <para>-can move one step diagonally</para>
///     <para>-can capture in the following pattern: one step orthogonally followed by the square directly above on level 2</para>
///     <para>Level 2: -can move and capture in the following pattern: the square directly below on level 1 followed by one step orthogonally</para>
/// </summary>
public class Elemental : ChessPiece
{
    public Elemental(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        return moves;
    }
}