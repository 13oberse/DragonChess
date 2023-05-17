using System.Collections.Generic;

namespace DragonChess.Piece;

public class Position
{
    public Position(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }

    public void CopyPos(Position pos)
    {
        X = pos.X;
        Y = pos.Y;
        Z = pos.Z;
    }

    public void NewPos(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}

public abstract class ChessPiece
{
    protected bool immobile;
    // TODO: some indicator for image source

    protected ChessPiece(bool white, int x, int y, int z)
    {
        Position = new Position(x, y, z);
        LastPosition = new Position(x, y, z);
        Owner = white;
    }

    public Position Position { get; set; }
    protected Position LastPosition { get; set; }
    public bool Owner { get; set; }

    public abstract List<Position> ValidMoves(ChessPiece[,,] board);

    public virtual List<Position> RemoteCaptures(ChessPiece[,,] board) => new();

    public virtual void MoveTo(ChessPiece?[,,] board, int x, int y, int z)
    {
        // Update the local current and last positions
        LastPosition.CopyPos(Position);
        Position.NewPos(x, y, z);

        // Update the board (note: this currently loses references to any captured pieces)
        board[Position.X, Position.Y, Position.Z] = this;
        board[LastPosition.X, LastPosition.Y, LastPosition.Z] = null;
    }
}

/* -------------------- UPPER LEVEL PIECES -------------------- */

/**
 * Sylph movement:
 * Level 3: -can move one step diagonally forward, or capture one step straight forward
 * -can capture on the square directly below on level 2
 * Level 2: -can move to the square directly above on level 3,
 * or to one of the player's six Sylph starting squares
 */
public class Sylph : ChessPiece
{
    public Sylph(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        if (Position.Z == 1)
        {
            /* Can move up if space above is unoccupied */
            if (board[Position.X, Position.Y, 2] == null)
            {
                moves.Add(new Position(Position.X, Position.Y, 2));
            }

            /* Can move to any of the starting sylph positions */
            var sylphStartY = Owner ? 1 : 6;
            for (var x = 0; x <= 10; x += 2)
            {
                if (board[x, sylphStartY, 2] == null)
                {
                    moves.Add(new Position(x, sylphStartY, 2));
                }
            }
        }
        else if (Position.Z == 2)
        {
            /* Can capture directly below */
            if (board[Position.X, Position.Y, 1] != null &&
                board[Position.X, Position.Y, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y, 1));
            }

            /* Index the Y-line in front of the piece */
            var nextYLine = Position.Y + (Owner ? 1 : -1);
            if (nextYLine < 0 || nextYLine >= 8)
            {
                return moves; // Can't move forward
            }

            /* Can capture directly forward */
            if (board[Position.X, nextYLine, 2] != null &&
                board[Position.X, nextYLine, 2].Owner != Owner)
            {
                moves.Add(new Position(Position.X, nextYLine, 2));
            }

            /* Can move diagonally foward */
            if (Position.X < 12 && board[Position.X + 1, nextYLine, 2] == null)
            {
                moves.Add(new Position(Position.X + 1, nextYLine, 2));
            }

            if (Position.X > 0 && board[Position.X - 1, nextYLine, 2] == null)
            {
                moves.Add(new Position(Position.X - 1, nextYLine, 2));
            }
        }

        /* Illegal position */
        return moves;
    }
}

/**
 * Griffon movement:
 * Level 3: -can move and capture like a knight, except it's a (2,3) jump
 * -can move and capture one step triagonally to level 2
 * Level 2: -can move and capture one step diagonally
 * -can move and capture one step triagonally to level 3
 */
public class Griffon : ChessPiece
{
    public Griffon(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
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

/**
 * Dragon Movement:
 * Level 3: -can move and capture as a chess king+bishop
 * -can capture remotely (without moving) on the square
 * directly below on level 2, or on any square
 * orthogonally adjacent to that square
 */
public class Dragon : ChessPiece
{
    public Dragon(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile || Position.Z != 2)
        {
            return moves; // Immobile or illegal position
        }

        /* Can move and capture as a chess bishop */
        /* Loop through all valid distances for each direction */
        for (var i = 1; i < 8; i++) // -x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0 || Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X - i, Position.Y - i, 2] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X - i, Position.Y - i, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y - i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y - i, 2));
        }

        for (var i = 1; i < 8; i++) // -x, +y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0 || Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X - i, Position.Y + i, 2] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X - i, Position.Y + i, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y + i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y + i, 2));
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12 || Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X + i, Position.Y - i, 2] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X + i, Position.Y - i, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y - i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y - i, 2));
        }

        for (var i = 1; i < 8; i++) // +x, -y direction
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12 || Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X + i, Position.Y + i, 2] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X + i, Position.Y + i, 2].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y + i, 2));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y + i, 2));
        }

        /* Can also move and capture as a chess king */
        /* Since diagonals are covered above, only orthogonal spaces need checked */
        if (Position.X - 1 >= 0)
        {
            if (board[Position.X - 1, Position.Y, 2] == null ||
                board[Position.X - 1, Position.Y, 2].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y, 2));
            }
        }

        if (Position.X + 1 < 12)
        {
            if (board[Position.X + 1, Position.Y, 2] == null ||
                board[Position.X + 1, Position.Y, 2].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y, 2));
            }
        }

        if (Position.Y - 1 >= 0)
        {
            if (board[Position.X, Position.Y - 1, 2] == null ||
                board[Position.X, Position.Y - 1, 2].Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y - 1, 2));
            }
        }

        if (Position.Y + 1 < 8)
        {
            if (board[Position.X, Position.Y + 1, 2] == null ||
                board[Position.X, Position.Y + 1, 2].Owner != Owner)
            {
                moves.Add(new Position(Position.X, Position.Y + 1, 2));
            }
        }

        return moves;
    }

    public override List<Position> RemoteCaptures(ChessPiece[,,] board)
    {
        var moves = new List<Position>();
        if (immobile)
        {
            return moves;
        }

        /* Can remotely capture any piece in the 3x3 below it */
        if (Position.Z == 2)
        {
            /* Loop through 3x3 area below this piece */
            for (var i = -1; i <= 1; i++)
            {
                for (var j = -1; j <= 1; j++)
                {
                    /* Ensure this square is on the board */
                    if (Position.X + i >= 0 && Position.Y + j >= 0 &&
                        Position.X + i < 12 && Position.Y + j < 8)
                        /* If there is an enemy piece here, it can be captured */
                    {
                        if (board[Position.X + i, Position.Y + j, 1] != null &&
                            board[Position.X + i, Position.Y + j, 1].Owner != Owner)
                        {
                            moves.Add(new Position(Position.X + i, Position.Y + i, 1));
                        }
                    }
                }
            }
        }

        /* Illegal position */
        return moves;
    }
}

/* -------------------- MIDDLE LEVEL PIECES -------------------- */

/**
 * Warrior Movement:
 * Level 2: -can move and capture like a chess pawn but without the initial two-step option
 * Note: promotes to Hero when reaching the furthest rank
 */
public class Warrior : ChessPiece
{
    public Warrior(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        if (Position.Z == 1)
        {
            /* Can move like a chess pawn (but without the initial 2-step option) */
            /* Calculate the row it can move to */
            var nextYLine = Position.Y + (Owner ? 1 : -1);
            if (nextYLine < 0 || nextYLine >= 8)
            {
                return moves; // Illegal position/should've been promoted
            }

            /* Can move (but not capture) directly ahead */
            if (board[Position.X, nextYLine, 1] == null)
            {
                moves.Add(new Position(Position.X, nextYLine, 1));
            }

            /* Can capture one step diagonally forward */
            if (Position.X - 1 >= 0 && board[Position.X - 1, nextYLine, 1] != null)
            {
                moves.Add(new Position(Position.X - 1, nextYLine, 1));
            }

            if (Position.X + 1 < 12 && board[Position.X + 1, nextYLine, 1] != null)
            {
                moves.Add(new Position(Position.X + 1, nextYLine, 1));
            }
        }

        /* Invalid position */
        return moves;
    }
}

/**
 * Oliphant Movement:
 * Level 2: -can move and capture like a chess rook
 */
public class Oliphant : ChessPiece
{
    public Oliphant(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile || Position.Z != 1)
        {
            return moves; // Immobilized or illegal position
        }

        /* Can move and capture like a chess rook */
        /* Loop through each direction until a piece (or the edge of the board) blocks movement */
        /* Negative horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.X - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X - i, Position.Y, 1] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X - i, Position.Y, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X - i, Position.Y, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X - i, Position.Y, 1));
        }

        /* Positive horizontal direction */
        for (var i = 1; i < 12; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.X + i >= 12)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X + i, Position.Y, 1] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X + i, Position.Y, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X + i, Position.Y, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X + i, Position.Y, 1));
        }

        /* Negative vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.Y - i < 0)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X, Position.Y - i, 1] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X, Position.Y - i, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X, Position.Y - i, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X, Position.Y - i, 1));
        }

        /* Positive vertical direction */
        for (var i = 1; i < 8; i++)
        {
            /* If this position is off the board, break out of loop */
            if (Position.Y + i >= 8)
            {
                break;
            }

            /* If there's a piece here blocking further movement, break out */
            if (board[Position.X, Position.Y + i, 1] != null)
            {
                /* If it's an enemy piece, it can be captured */
                if (board[Position.X, Position.Y + i, 1].Owner != Owner)
                {
                    moves.Add(new Position(Position.X, Position.Y + i, 1));
                }

                break;
            }

            /* Nothing has blocked movement in this direction yet */
            moves.Add(new Position(Position.X, Position.Y + i, 1));
        }

        return moves;
    }
}

/**
 * Unicorn Movement:
 * Level 2: -can move and capture like a chess knight
 */
public class Unicorn : ChessPiece
{
    public Unicorn(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile || Position.Z != 1)
        {
            return moves; // Immobile or illegal position
        }

        /* Can move/capture like a chess knight */
        if (Position.X - 1 >= 0 && Position.Y - 2 >= 0)
        {
            if (board[Position.X - 1, Position.Y - 2, 1] == null ||
                board[Position.X - 1, Position.Y - 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y - 2, 1));
            }
        }

        if (Position.X - 2 >= 0 && Position.Y - 1 >= 0)
        {
            if (board[Position.X - 2, Position.Y - 1, 1] == null ||
                board[Position.X - 2, Position.Y - 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 2, Position.Y - 1, 1));
            }
        }

        if (Position.X - 1 >= 0 && Position.Y + 2 < 8)
        {
            if (board[Position.X - 1, Position.Y + 2, 1] == null ||
                board[Position.X - 1, Position.Y + 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 1, Position.Y + 2, 1));
            }
        }

        if (Position.X - 2 >= 0 && Position.Y + 1 < 8)
        {
            if (board[Position.X - 2, Position.Y + 1, 1] == null ||
                board[Position.X - 2, Position.Y + 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X - 2, Position.Y + 1, 1));
            }
        }

        if (Position.X + 1 < 12 && Position.Y - 2 >= 0)
        {
            if (board[Position.X + 1, Position.Y - 2, 1] == null ||
                board[Position.X + 1, Position.Y - 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y - 2, 1));
            }
        }

        if (Position.X + 2 < 12 && Position.Y - 1 >= 0)
        {
            if (board[Position.X + 2, Position.Y - 1, 1] == null ||
                board[Position.X + 2, Position.Y - 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 2, Position.Y - 1, 1));
            }
        }

        if (Position.X + 1 < 12 && Position.Y + 2 < 8)
        {
            if (board[Position.X + 1, Position.Y + 2, 1] == null ||
                board[Position.X + 1, Position.Y + 2, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 1, Position.Y + 2, 1));
            }
        }

        if (Position.X + 2 < 12 && Position.Y + 1 < 8)
        {
            if (board[Position.X + 2, Position.Y + 1, 1] == null ||
                board[Position.X + 2, Position.Y + 1, 1].Owner != Owner)
            {
                moves.Add(new Position(Position.X + 2, Position.Y + 1, 1));
            }
        }

        return moves;
    }
}

/**
 * Hero Movement: [[WIP]]
 * Level   2:  -can move and capture one or two unblockable steps diagonally
 * -can move and capture one step triagonally to levels 1 or 3
 * Levels 1&3: -can move and capture back to the square on level 2 the Hero previously left
 */
public class Hero : ChessPiece
{
    public Hero(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * Thief Movement: [[WIP]]
 * Level 2: -can move and capture like a chess bishop
 */
public class Thief : ChessPiece
{
    public Thief(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * Cleric Movement: [[WIP]]
 * Any Level: -can move and capture like a chess king
 * -can move and capture to the square directly above or
 * directly below on an adjacent level
 */
public class Cleric : ChessPiece
{
    public Cleric(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * Mage Movement: [[WIP]]
 * Level   2:  -can move and capture like a chess queen
 * Levels 1&3: -can move one step orthogonally
 * Any Level:  -can move and capture one or two steps (blockable) directly
 * above or directly below to one of the other levels
 */
public class Mage : ChessPiece
{
    public Mage(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * King Movement: [[WIP]]
 * Level   2:  -can move and capture like a chess king
 * -can move and capture to the square directly below on level 1 or directly above on level 3
 * Levels 1&3: -can move to (only) the same square on level 2 the King previously left
 */
public class King : ChessPiece
{
    public King(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * Paladin Movement: [[WIP]]
 * Level   2:  -can move and capture as a chess king+knight
 * Levels 1&3: -can move and capture like a chess king
 * Any Level:  -can move to the other levels using a knight-like move:
 * -one level up or down followed by two steps orthogonally
 * -two levels up or down followed by one step orthogonally
 */
public class Paladin : ChessPiece
{
    public Paladin(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/* -------------------- LOWER LEVEL PIECES -------------------- */

/**
 * Dwarf Movement [[WIP]]
 * Level 1: -can move one step straight forward or sideways, or capture one step diagonally forward
 * -can capture on the square directly above on level 2
 * Level 2: -can move one step straight forward or sideways, or capture one step diagonally forward
 * -can move to the square directly below on level 1
 */
public class Dwarf : ChessPiece
{
    public Dwarf(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }
}

/**
 * Basilisk Movement [[WIP]]
 * Level 1: -can move and capture one step diagonally forward or straight forward
 * -can move one step straight backward
 * Note: automatically freezes (immobilizes) an enemy piece on the square directly
 * above on level 2, whether the Basilisk moves to the space below or the enemy
 * moves to the space above, and until the Basilisk moves away or is captured
 */
public class Basilisk : ChessPiece
{
    public Basilisk(bool white, int x, int y, int z) : base(white, x, y, z)
    {
    }

    public override List<Position> ValidMoves(ChessPiece[,,] board)
    {
        List<Position> moves = new();
        if (immobile)
        {
            return moves;
        }

        return moves;
    }

    public override void MoveTo(ChessPiece?[,,] board, int x, int y, int z)
    {
        // TODO: immobilize/remobilize pieces
        LastPosition.CopyPos(Position);
        Position.NewPos(x, y, z);

        // Update the board (note: this currently loses references to any captured pieces)
        board[Position.X, Position.Y, Position.Z] = this;
        board[LastPosition.X, LastPosition.Y, LastPosition.Z] = null;
    }
}

/**
 * Elemental Movement [[WIP]]
 * Level 1: -can move and capture one or two steps orthogonally
 * -can move one step diagonally
 * -can capture in the following pattern: one step orthogonally followed by the square directly above on level 2
 * Level 2: -can move and capture in the following pattern: the square directly below on level 1 followed by one step orthogonally
 */
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