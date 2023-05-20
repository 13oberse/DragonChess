using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

public abstract class ChessPiece
{
    protected bool Immobile { get; set; }
    public Position Position { get; set; }
    protected Position LastPosition { get; set; }

    public bool Owner { get; set; }
    // TODO: some indicator for image source

    protected ChessPiece(bool white, int x, int y, int z)
    {
        Position = new Position(x, y, z);
        LastPosition = new Position(x, y, z);
        Owner = white;
    }

    public abstract List<Position> ValidMoves(ChessPiece?[,,] board);

    public virtual List<Position> RemoteCaptures(ChessPiece?[,,] board) => new();

    public virtual void MoveTo(ChessPiece?[,,] board, int x, int y, int z)
    {
        // Update the local current and last positions
        LastPosition.CopyPos(Position);
        Position.NewPos(x, y, z);

        // Update the board (note: this currently loses references to any captured pieces)
        board[Position.X, Position.Y, Position.Z] = this;
        board[LastPosition.X, LastPosition.Y, LastPosition.Z] = null;
    }

    protected void CheckMove(ChessPiece?[,,] board, List<Position> moves, int x, int y, int z)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return;
        }

        var positionToCheck = board[x, y, z];
        if (positionToCheck == null || positionToCheck.Owner != Owner)
        {
            moves.Add(new Position(x, y, z));
        }
    }
}