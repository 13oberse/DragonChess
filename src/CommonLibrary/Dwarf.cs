using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Dwarf Movement</para>
///     <para>Level 1: -can move one step straight forward or sideways, or capture one step diagonally forward</para>
///     <para>-can capture on the square directly above on level 2</para>
///     <para>Level 2: -can move one step straight forward or sideways, or capture one step diagonally forward</para>
///     <para>-can move to the square directly below on level 1</para>
/// </summary>
public class Dwarf : ChessPiece
{
    public Dwarf(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece?[,,] board)
    {
        var moves = new List<Position>();
        if (Immobile)
        {
            return moves;
        }

        return moves;
    }
}