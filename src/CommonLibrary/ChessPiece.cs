namespace DragonChess.Piece
{
    public class ChessPos
    {
        public int x;
        public int y;
        public int z;

        public ChessPos(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public void CopyPos(ChessPos pos)
        {
            this.x = pos.x;
            this.y = pos.y;
            this.z = pos.z;
        }

        public void NewPos(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }

    public abstract class ChessPiece
    {
        public bool owner;
        public bool immobile;
        public ChessPos pos;
        public ChessPos lastPos;
        // TODO: some indicator for image source?

        public ChessPiece(bool white, int x, int y, int z)
        {
            pos = new ChessPos(x, y, z);
            lastPos = new ChessPos(x, y, z);
            owner = white;
        }

        public abstract List<ChessPos> ValidMoves();
        public virtual List<ChessPos> RemoteCaptures()
        {
            return new List<ChessPos>();
        }
        public virtual void MoveTo(int x, int y, int z)
        {
            lastPos.CopyPos(pos);
            pos.NewPos(x, y, z);
        }
    }

    /* -------------------- UPPER LEVEL PIECES -------------------- */

    /** Sylph movement: [[WIP]]
     *  Level 3: -can move one step diagonally forward, or capture one step straight forward
     *           -can capture on the square directly below on level 2
     *  Level 2: -can move to the square directly above on level 3,
     *              or to one of the player's six Sylph starting squares
     */
    public class Sylph : ChessPiece
    {
        public Sylph(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }

            if (this.pos.z == 1)
            {
                if (/*space above is unoccupied (not null)*/true)
                {
                    moves.Add(new ChessPos(this.pos.x, this.pos.y, 2));
                }
                /*similarly check the starting sylph positions*/
            }
            else if (this.pos.z == 2)
            {

            }
            else
            {
                /* Illegal position */
            }
            return moves;
        }
    }

    /** Griffon movement: [[WIP]]
     *  Level 3: -can move and capture like a knight, except it's a (2,3) jump
     *           -can move and capture one step triagonally to level 2
     *  Level 2: -can move and capture one step diagonally
     *           -can move and capture one step triagonally to level 3
     */
    public class Griffon : ChessPiece
    {
        public Griffon(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Dragon Movement: [[WIP]]
     *  Level 3: -can move and capture as a chess king+bishop
     *           -can capture remotely (without moving) on the square
     *              directly below on level 2, or on any square
     *              orthogonally adjacent to that square
     */
    public class Dragon : ChessPiece
    {
        public Dragon(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }

        public override List<ChessPos> RemoteCaptures()
        {
            List<ChessPos> moves = new List<ChessPos>();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /* -------------------- MIDDLE LEVEL PIECES -------------------- */

    /** Warrior Movement: [[WIP]]
     *  Level 2: -can move and capture like a chess pawn but without the initial two-step option
     *  Note: promotes to Hero when reaching the furthest rank
     */
    public class Warrior : ChessPiece
    {
        public Warrior(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Oliphant Movement: [[WIP]]
     *  Level 2: -can move and capture like a chess rook
     */
    public class Oliphant : ChessPiece
    {
        public Oliphant(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Unicorn Movement: [[WIP]]
     *  Level 2: -can move and capture like a chess knight
     */
    public class Unicorn : ChessPiece
    {
        public Unicorn(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Hero Movement: [[WIP]]
     *  Level   2:  -can move and capture one or two unblockable steps diagonally
     *              -can move and capture one step triagonally to levels 1 or 3
     *  Levels 1&3: -can move and capture back to the square on level 2 the Hero previously left
     */
    public class Hero : ChessPiece
    {
        public Hero(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Thief Movement: [[WIP]]
     *  Level 2: -can move and capture like a chess bishop
     */
    public class Thief : ChessPiece
    {
        public Thief(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Cleric Movement: [[WIP]]
     *  Any Level: -can move and capture like a chess king
     *             -can move and capture to the square directly above or
     *               directly below on an adjacent level
     */
    public class Cleric : ChessPiece
    {
        public Cleric(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Mage Movement: [[WIP]]
     *  Level   2:  -can move and capture like a chess queen
     *  Levels 1&3: -can move one step orthogonally
     *  Any Level:  -can move and capture one or two steps (blockable) directly
     *                above or directly below to one of the other levels
     */
    public class Mage : ChessPiece
    {
        public Mage(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** King Movement: [[WIP]]
     *  Level   2:  -can move and capture like a chess king
     *              -can move and capture to the square directly below on level 1 or directly above on level 3
     *  Levels 1&3: -can move to (only) the same square on level 2 the King previously left
     */
    public class King : ChessPiece
    {
        public King(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Paladin Movement: [[WIP]]
     *  Level   2:  -can move and capture as a chess king+knight
     *  Levels 1&3: -can move and capture like a chess king
     *  Any Level:  -can move to the other levels using a knight-like move:
     *                -one level up or down followed by two steps orthogonally
     *                -two levels up or down followed by one step orthogonally
     */
    public class Paladin : ChessPiece
    {
        public Paladin(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /* -------------------- LOWER LEVEL PIECES -------------------- */

    /** Dwarf Movement [[WIP]]
     *  Level 1: -can move one step straight forward or sideways, or capture one step diagonally forward
     *           -can capture on the square directly above on level 2
     *  Level 2: -can move one step straight forward or sideways, or capture one step diagonally forward
     *           -can move to the square directly below on level 1
     */
    public class Dwarf : ChessPiece
    {
        public Dwarf(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }
    }

    /** Basilisk Movement [[WIP]]
     *  Level 1: -can move and capture one step diagonally forward or straight forward
     *           -can move one step straight backward
     *  Note: automatically freezes (immobilizes) an enemy piece on the square directly
     *        above on level 2, whether the Basilisk moves to the space below or the enemy
     *        moves to the space above, and until the Basilisk moves away or is captured
     */
    public class Basilisk : ChessPiece
    {
        public Basilisk(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            if (this.immobile)
            {
                return moves;
            }
            return moves;
        }

        public override void MoveTo(int x, int y, int z)
        {
            // TODO: immobilize/remobilize pieces
            lastPos.CopyPos(pos);
            pos.NewPos(x, y, z);
        }
    }

    /** Elemental Movement [[WIP]]
     *  Level 1: -can move and capture one or two steps orthogonally
     *           -can move one step diagonally
     *           -can capture in the following pattern: one step orthogonally followed by the square directly above on level 2
     *  Level 2: -can move and capture in the following pattern: the square directly below on level 1 followed by one step orthogonally
     */
    public class Elemental : ChessPiece
    {
        public Elemental(bool white, int x, int y, int z) : base(white, x, y, z) { }

        public override List<ChessPos> ValidMoves()
        {
            List<ChessPos> moves = new();
            return moves;
        }
    }
}