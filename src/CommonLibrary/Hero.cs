using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

/// <summary>
///     <para>Hero Movement</para>
///     <para>Level 2: -can move and capture one or two unblockable steps diagonally</para>
///     <para>-can move and capture one step triagonally to levels 1 or 3</para>
///     <para>Levels 1 &amp; 3: -can move and capture back to the square on level 2 the Hero previously left</para>
/// </summary>
public class Hero : ChessPiece
{
    public Hero(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    //TODO
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