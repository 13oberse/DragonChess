using System;
using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

public abstract class ChessPiece
{
	public Position Position { get; set; }
	public Position LastPosition { get; set; }

	public PlayerColor Owner { get; set; }

	public int ImgID { get; set; }

	protected ChessPiece(PlayerColor color, int x, int y, int z)
	{
		Position = new Position(x, y, z);
		LastPosition = new Position(x, y, z);
		Owner = color;
	}

	public abstract List<Position> ValidMoves(ChessPiece?[,,] board);

	public virtual List<Position> RemoteCaptures(ChessPiece?[,,] board) => [];

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

	protected bool IsImmobilized(ChessPiece?[,,] board)
	{
		if (Position.Z == 0)
			return false;
		ChessPiece? piece = board[Position.X, Position.Y, Position.Z - 1];
		if (piece == null)
			return false;
		return piece.GetType().Equals(typeof(Basilisk)) && piece.Owner != Owner;
	}

	// Return list of standard bishop moves
	protected List<Position> GetBishopMoves(ChessPiece?[,,] board)
	{
		var moves = new List<Position>();

		/* Loop through all valid distances for each direction */
		for (var i = 1; i < 8; i++) // -x, -y direction
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X - i, Position.Y - i, Position.Z, MoveType.MoveCapture))
				break;
		}

		for (var i = 1; i < 8; i++) // -x, +y direction
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X - i, Position.Y + i, Position.Z, MoveType.MoveCapture))
				break;
		}

		for (var i = 1; i < 8; i++) // +x, -y direction
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X + i, Position.Y - i, Position.Z, MoveType.MoveCapture))
				break;
		}

		for (var i = 1; i < 8; i++) // +x, -y direction
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X + i, Position.Y + i, Position.Z, MoveType.MoveCapture))
				break;
		}

		return moves;
	}

	// Return list of standard rook moves
	protected List<Position> GetRookMoves(ChessPiece?[,,] board)
	{
		var moves = new List<Position>();

		/* Loop through each direction until a piece (or the edge of the board) blocks movement */
		/* Negative horizontal direction */
		for (var i = 1; i < 12; i++)
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X - i, Position.Y, Position.Z, MoveType.MoveCapture))
				break;
		}

		/* Positive horizontal direction */
		for (var i = 1; i < 12; i++)
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X + i, Position.Y, Position.Z, MoveType.MoveCapture))
				break;
		}

		/* Negative vertical direction */
		for (var i = 1; i < 8; i++)
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X, Position.Y - i, Position.Z, MoveType.MoveCapture))
				break;
		}

		/* Positive vertical direction */
		for (var i = 1; i < 8; i++)
		{
			/* If there's a piece there, break out of loop */
			if (!CheckMove(board, moves, Position.X, Position.Y + i, Position.Z, MoveType.MoveCapture))
				break;
		}

		return moves;
	}

	// Return list of standard king moves
	protected List<Position> GetKingMoves(ChessPiece?[,,] board)
	{
		var moves = new List<Position>();

		CheckMove(board, moves, Position.X + 1, Position.Y + 1, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X + 1, Position.Y,     Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X + 1, Position.Y - 1, Position.Z, MoveType.MoveCapture);

		CheckMove(board, moves, Position.X,     Position.Y + 1, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X,     Position.Y - 1, Position.Z, MoveType.MoveCapture);

		CheckMove(board, moves, Position.X - 1, Position.Y + 1, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X - 1, Position.Y,     Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X - 1, Position.Y - 1, Position.Z, MoveType.MoveCapture);

		return moves;
	}

	// Return list of standard(ish) chess jump moves (standard knight is 1,2)
	protected List<Position> GetJumpMoves(ChessPiece?[,,] board, int a, int b)
	{
		var moves = new List<Position>();

		CheckMove(board, moves, Position.X - a, Position.Y - b, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X - b, Position.Y - a, Position.Z, MoveType.MoveCapture);

		CheckMove(board, moves, Position.X - a, Position.Y + b, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X - b, Position.Y + a, Position.Z, MoveType.MoveCapture);

		CheckMove(board, moves, Position.X + a, Position.Y - b, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X + b, Position.Y - a, Position.Z, MoveType.MoveCapture);

		CheckMove(board, moves, Position.X + a, Position.Y + b, Position.Z, MoveType.MoveCapture);
		CheckMove(board, moves, Position.X + b, Position.Y + a, Position.Z, MoveType.MoveCapture);

		return moves;
	}
}
