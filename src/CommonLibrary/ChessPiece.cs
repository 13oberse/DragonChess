using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

public abstract class ChessPiece
{
    public bool Immobile { get; set; }
    public Position Position { get; set; }
    public Position LastPosition { get; set; }

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

    /// <summary>
    /// Takes a position, and adds it to 'moves' if the position is an empty square or has an enemy piece
    /// </summary>
    /// <param name="board"></param>
    /// <param name="moves"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="z"></param>
    /// <returns>True if the given position is an empty square</returns>
    protected bool CheckMove(ChessPiece?[,,] board, List<Position> moves, int x, int y, int z, MoveType moveType)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return false;
        }

        /* Grab position to check from the board */
        var positionToCheck = board[x, y, z];

        /* Flag for whether the given position is a valid move */
        bool validMove = false;

        /* Set validMove flag depending on moveType */
        switch (moveType)
        {
            case MoveType.MoveOnly:
                validMove = (positionToCheck == null);
                break;
            case MoveType.CaptureOnly:
                validMove = (positionToCheck != null && positionToCheck.Owner != Owner);
                break;
            case MoveType.MoveCapture:
                validMove = (positionToCheck == null || positionToCheck.Owner != Owner);
                break;
        }

        /* If move is valid, add to moves */
        if (validMove)
        {
            moves.Add(new Position(x, y, z));
        }

        /* return true if position is empty */
        return positionToCheck == null;
    }
}