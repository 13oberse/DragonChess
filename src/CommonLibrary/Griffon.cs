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

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (Immobile)
        {
            return moves;
        }

        if (Position.Z == 2)
        {
            /* Can move and capture like a knight, but in a (2,3) jump */
            /* Screw it, check them manually */
            if (Position.X - 2 >= 0 && Position.Y - 3 >= 0)
            {
                if (board[Position.X - 2, Position.Y - 3, 2] == null ||
                    board[Position.X - 2, Position.Y - 3, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 2, Position.Y - 3, 2));
                }
            }

            if (Position.X - 3 >= 0 && Position.Y - 2 >= 0)
            {
                if (board[Position.X - 3, Position.Y - 2, 2] == null ||
                    board[Position.X - 3, Position.Y - 2, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 3, Position.Y - 2, 2));
                }
            }

            if (Position.X - 2 >= 0 && Position.Y + 3 < 8)
            {
                if (board[Position.X - 2, Position.Y + 3, 2] == null ||
                    board[Position.X - 2, Position.Y + 3, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 2, Position.Y + 3, 2));
                }
            }

            if (Position.X - 3 >= 0 && Position.Y + 2 < 8)
            {
                if (board[Position.X - 3, Position.Y + 2, 2] == null ||
                    board[Position.X - 3, Position.Y + 2, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 3, Position.Y + 2, 2));
                }
            }

            if (Position.X + 2 < 12 && Position.Y - 3 >= 0)
            {
                if (board[Position.X + 2, Position.Y - 3, 2] == null ||
                    board[Position.X + 2, Position.Y - 3, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 2, Position.Y - 3, 2));
                }
            }

            if (Position.X + 3 < 12 && Position.Y - 2 >= 0)
            {
                if (board[Position.X + 3, Position.Y - 2, 2] == null ||
                    board[Position.X + 3, Position.Y - 2, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 3, Position.Y - 2, 2));
                }
            }

            if (Position.X + 2 < 12 && Position.Y + 3 < 8)
            {
                if (board[Position.X + 2, Position.Y + 3, 2] == null ||
                    board[Position.X + 2, Position.Y + 3, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 2, Position.Y + 3, 2));
                }
            }

            if (Position.X + 3 < 12 && Position.Y + 2 < 8)
            {
                if (board[Position.X + 3, Position.Y + 2, 2] == null ||
                    board[Position.X + 3, Position.Y + 2, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 3, Position.Y + 2, 2));
                }
            }

            /* And can move and capture triagonally to the middle level */
            if (Position.X - 1 >= 0 && Position.Y - 1 >= 0)
            {
                if (board[Position.X - 1, Position.Y - 1, 1] == null ||
                    board[Position.X - 1, Position.Y - 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y - 1, 1));
                }
            }

            if (Position.X - 1 >= 0 && Position.Y + 1 < 8)
            {
                if (board[Position.X - 1, Position.Y + 1, 1] == null ||
                    board[Position.X - 1, Position.Y + 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y + 1, 1));
                }
            }

            if (Position.X + 1 < 12 && Position.Y - 1 >= 0)
            {
                if (board[Position.X + 1, Position.Y - 1, 1] == null ||
                    board[Position.X + 1, Position.Y - 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y - 1, 1));
                }
            }

            if (Position.X + 1 < 12 && Position.Y + 1 < 8)
            {
                if (board[Position.X + 1, Position.Y + 1, 1] == null ||
                    board[Position.X + 1, Position.Y + 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y + 1, 1));
                }
            }
        }

        if (Position.Z == 1)
        {
            /* Can move and capture one step diagonally or */
            /* move and capture one step triagonally to the top level */
            if (Position.X - 1 >= 0 && Position.Y - 1 >= 0)
            {
                if (board[Position.X - 1, Position.Y - 1, 1] == null ||
                    board[Position.X - 1, Position.Y - 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y - 1, 1));
                }

                if (board[Position.X - 1, Position.Y - 1, 2] == null ||
                    board[Position.X - 1, Position.Y - 1, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y - 1, 2));
                }
            }

            if (Position.X - 1 >= 0 && Position.Y + 1 < 8)
            {
                if (board[Position.X - 1, Position.Y + 1, 1] == null ||
                    board[Position.X - 1, Position.Y + 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y + 1, 1));
                }

                if (board[Position.X - 1, Position.Y + 1, 2] == null ||
                    board[Position.X - 1, Position.Y + 1, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - 1, Position.Y + 1, 2));
                }
            }

            if (Position.X + 1 < 12 && Position.Y - 1 >= 0)
            {
                if (board[Position.X + 1, Position.Y - 1, 1] == null ||
                    board[Position.X + 1, Position.Y - 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y - 1, 1));
                }

                if (board[Position.X + 1, Position.Y - 1, 2] == null ||
                    board[Position.X + 1, Position.Y - 1, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y - 1, 2));
                }
            }

            if (Position.X + 1 < 12 && Position.Y + 1 < 8)
            {
                if (board[Position.X + 1, Position.Y + 1, 1] == null ||
                    board[Position.X + 1, Position.Y + 1, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y + 1, 1));
                }

                if (board[Position.X + 1, Position.Y + 1, 2] == null ||
                    board[Position.X + 1, Position.Y + 1, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + 1, Position.Y + 1, 2));
                }
            }
        }

        /* Illegal position */
        return moves;
    }
}