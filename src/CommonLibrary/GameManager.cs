﻿using System;
using System.Collections.Generic;

namespace DragonChess.CommonLibrary;

public class GameManager
{
    private static ChessPiece?[,,] board = new ChessPiece[12, 8, 3];

    private static PlayerColor turnPlayer = PlayerColor.White;

    public ChessPiece? GetPiece(int x, int y, int z)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return null;
        }
        return board[x, y, z];
    }

    public void SetPiece(ChessPiece piece, int x, int y, int z)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return;
        }
        board[x, y, z] = piece;
    }

    public PlayerColor GetTurnPlayer()
    {
        return turnPlayer;
    }

    public void ResetBoard()
    {
        for (int x=0; x<12; x++)
        {
            for (int y=0; y<8; y++)
            {
                for (int z=0; z<3; z++)
                {
                    board[x, y, z] = null;
                }
            }
        }

        // Reset turn player
        turnPlayer = PlayerColor.White;

        // White pieces (sky)
        board[2, 0, 2] = new Griffon(PlayerColor.White, 2, 0, 2);
        board[6, 0, 2] = new Dragon(PlayerColor.White, 6, 0, 2);
        board[10, 0, 2] = new Griffon(PlayerColor.White, 10, 0, 2);
        board[0, 1, 2] = new Sylph(PlayerColor.White, 0, 1, 2);
        board[2, 1, 2] = new Sylph(PlayerColor.White, 2, 1, 2);
        board[4, 1, 2] = new Sylph(PlayerColor.White, 4, 1, 2);
        board[6, 1, 2] = new Sylph(PlayerColor.White, 6, 1, 2);
        board[8, 1, 2] = new Sylph(PlayerColor.White, 8, 1, 2);
        board[10, 1, 2] = new Sylph(PlayerColor.White, 10, 1, 2);

        // White pieces (surface)
        board[0, 0, 1] = new Oliphant(PlayerColor.White, 0, 0, 1);
        board[1, 0, 1] = new Unicorn(PlayerColor.White, 1, 0, 1);
        board[2, 0, 1] = new Hero(PlayerColor.White, 2, 0, 1);
        board[3, 0, 1] = new Thief(PlayerColor.White, 3, 0, 1);
        board[4, 0, 1] = new Cleric(PlayerColor.White, 4, 0, 1);
        board[5, 0, 1] = new Mage(PlayerColor.White, 5, 0, 1);
        board[6, 0, 1] = new King(PlayerColor.White, 6, 0, 1);
        board[7, 0, 1] = new Paladin(PlayerColor.White, 7, 0, 1);
        board[8, 0, 1] = new Thief(PlayerColor.White, 8, 0, 1);
        board[9, 0, 1] = new Hero(PlayerColor.White, 9, 0, 1);
        board[10, 0, 1] = new Unicorn(PlayerColor.White, 10, 0, 1);
        board[11, 0, 1] = new Oliphant(PlayerColor.White, 11, 0, 1);
        board[0, 1, 1] = new Warrior(PlayerColor.White, 0, 1, 1);
        board[1, 1, 1] = new Warrior(PlayerColor.White, 1, 1, 1);
        board[2, 1, 1] = new Warrior(PlayerColor.White, 2, 1, 1);
        board[3, 1, 1] = new Warrior(PlayerColor.White, 3, 1, 1);
        board[4, 1, 1] = new Warrior(PlayerColor.White, 4, 1, 1);
        board[5, 1, 1] = new Warrior(PlayerColor.White, 5, 1, 1);
        board[6, 1, 1] = new Warrior(PlayerColor.White, 6, 1, 1);
        board[7, 1, 1] = new Warrior(PlayerColor.White, 7, 1, 1);
        board[8, 1, 1] = new Warrior(PlayerColor.White, 8, 1, 1);
        board[9, 1, 1] = new Warrior(PlayerColor.White, 9, 1, 1);
        board[10, 1, 1] = new Warrior(PlayerColor.White, 10, 1, 1);
        board[11, 1, 1] = new Warrior(PlayerColor.White, 11, 1, 1);

        // White pieces (underdark)
        board[2, 0, 0] = new Basilisk(PlayerColor.White, 2, 0, 0);
        board[6, 0, 0] = new Elemental(PlayerColor.White, 6, 0, 0);
        board[10, 0, 0] = new Basilisk(PlayerColor.White, 10, 0, 0);
        board[1, 1, 0] = new Dwarf(PlayerColor.White, 1, 1, 0);
        board[3, 1, 0] = new Dwarf(PlayerColor.White, 3, 1, 0);
        board[5, 1, 0] = new Dwarf(PlayerColor.White, 5, 1, 0);
        board[7, 1, 0] = new Dwarf(PlayerColor.White, 7, 1, 0);
        board[9, 1, 0] = new Dwarf(PlayerColor.White, 9, 1, 0);
        board[11, 1, 0] = new Dwarf(PlayerColor.White, 11, 1, 0);


        // Black pieces (sky)
        board[2, 7, 2] = new Griffon(PlayerColor.Black, 2, 7, 2);
        board[6, 7, 2] = new Dragon(PlayerColor.Black, 6, 7, 2);
        board[10, 7, 2] = new Griffon(PlayerColor.Black, 10, 7, 2);
        board[0, 6, 2] = new Sylph(PlayerColor.Black, 0, 6, 2);
        board[2, 6, 2] = new Sylph(PlayerColor.Black, 2, 6, 2);
        board[4, 6, 2] = new Sylph(PlayerColor.Black, 4, 6, 2);
        board[6, 6, 2] = new Sylph(PlayerColor.Black, 6, 6, 2);
        board[8, 6, 2] = new Sylph(PlayerColor.Black, 8, 6, 2);
        board[10, 6, 2] = new Sylph(PlayerColor.Black, 10, 6, 2);

        // Black pieces (surface)
        board[0, 7, 1] = new Oliphant(PlayerColor.Black, 0, 7, 1);
        board[1, 7, 1] = new Unicorn(PlayerColor.Black, 1, 7, 1);
        board[2, 7, 1] = new Hero(PlayerColor.Black, 2, 7, 1);
        board[3, 7, 1] = new Thief(PlayerColor.Black, 3, 7, 1);
        board[4, 7, 1] = new Cleric(PlayerColor.Black, 4, 7, 1);
        board[5, 7, 1] = new Mage(PlayerColor.Black, 5, 7, 1);
        board[6, 7, 1] = new King(PlayerColor.Black, 6, 7, 1);
        board[7, 7, 1] = new Paladin(PlayerColor.Black, 7, 7, 1);
        board[8, 7, 1] = new Thief(PlayerColor.Black, 8, 7, 1);
        board[9, 7, 1] = new Hero(PlayerColor.Black, 9, 7, 1);
        board[10, 7, 1] = new Unicorn(PlayerColor.Black, 10, 7, 1);
        board[11, 7, 1] = new Oliphant(PlayerColor.Black, 11, 7, 1);
        board[0, 6, 1] = new Warrior(PlayerColor.Black, 0, 6, 1);
        board[1, 6, 1] = new Warrior(PlayerColor.Black, 1, 6, 1);
        board[2, 6, 1] = new Warrior(PlayerColor.Black, 2, 6, 1);
        board[3, 6, 1] = new Warrior(PlayerColor.Black, 3, 6, 1);
        board[4, 6, 1] = new Warrior(PlayerColor.Black, 4, 6, 1);
        board[5, 6, 1] = new Warrior(PlayerColor.Black, 5, 6, 1);
        board[6, 6, 1] = new Warrior(PlayerColor.Black, 6, 6, 1);
        board[7, 6, 1] = new Warrior(PlayerColor.Black, 7, 6, 1);
        board[8, 6, 1] = new Warrior(PlayerColor.Black, 8, 6, 1);
        board[9, 6, 1] = new Warrior(PlayerColor.Black, 9, 6, 1);
        board[10, 6, 1] = new Warrior(PlayerColor.Black, 10, 6, 1);
        board[11, 6, 1] = new Warrior(PlayerColor.Black, 11, 6, 1);

        // Black pieces (underdark)
        board[2, 7, 0] = new Basilisk(PlayerColor.Black, 2, 7, 0);
        board[6, 7, 0] = new Elemental(PlayerColor.Black, 6, 7, 0);
        board[10, 7, 0] = new Basilisk(PlayerColor.Black, 10, 7, 0);
        board[1, 6, 0] = new Dwarf(PlayerColor.Black, 1, 6, 0);
        board[3, 6, 0] = new Dwarf(PlayerColor.Black, 3, 6, 0);
        board[5, 6, 0] = new Dwarf(PlayerColor.Black, 5, 6, 0);
        board[7, 6, 0] = new Dwarf(PlayerColor.Black, 7, 6, 0);
        board[9, 6, 0] = new Dwarf(PlayerColor.Black, 9, 6, 0);
        board[11, 6, 0] = new Dwarf(PlayerColor.Black, 11, 6, 0);
    }

    public List<Position> GetMoves(int x, int y, int z)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return new List<Position>();
        }
        ChessPiece? piece = board[x, y, z];
        if (piece != null)
        {
            return piece.ValidMoves(board);
        }
        return new List<Position>();
    }

    public List<Position> GetRemoteCaptures(int x, int y, int z)
    {
        if (x is < 0 or > 11 || y is < 0 or > 7 || z is < 0 or > 2)
        {
            return new List<Position>();
        }
        ChessPiece? piece = board[x, y, z];
        if (piece != null)
        {
            return piece.RemoteCaptures(board);
        }
        return new List<Position>();
    }

    // Applies the indicated move. Returns true if game over
    public CheckState DoMove(ChessPiece piece, int x, int y, int z)
    {
        if (piece.ValidMoves(board).Contains(new Position(x, y, z)))
        {
            piece.MoveTo(board, x, y, z);
            ToggleTurnPlayer();
            return MateCheck();
        }
        return CheckState.None;
    }

    // Applies the indicated capture. Returns true if game over
    public CheckState DoRemoteCapture(ChessPiece piece, int x, int y, int z)
    {
        if (piece.RemoteCaptures(board).Contains(new Position(x, y, z)))
        {
            board[x, y, z] = null;
            ToggleTurnPlayer();
            return MateCheck();
        }
        return CheckState.None;
    }

    // Check to see if turn player is in check/mate
    // TODO: implement
    private CheckState MateCheck()
    {
        Position? kingPosition = null;
        // Find king
        for (int x=0; x<12; x++)
        {
            for (int y=0; y<8; y++)
            {
                for (int z=0; z<3; z++)
                {
                    ChessPiece? piece = board[x, y, z];
                    if (piece != null && piece.Owner == turnPlayer &&
                        piece.GetType().Equals(typeof(King)))
                    {
                        kingPosition = new Position(x, y, z);
                        break;
                    }
                }
                if (kingPosition != null)
                    break;
            }
            if (kingPosition != null)
                break;
        }
        // If no king was found, return
        if (kingPosition == null)
            return CheckState.None; // error?

        // Now see if any pieces are threatening the king
        foreach (ChessPiece? piece in board)
        {
            if (piece != null)
            {
                // This feels inefficient (checks a lot of cells that aren't the king...)
                if (piece.ValidMoves(board).Contains(kingPosition) ||
                    piece.RemoteCaptures(board).Contains(kingPosition))
                {
                    // TODO: Could be checkmate
                    return CheckState.Check;
                }
            }
        }
        return CheckState.None;
    }

    private void ToggleTurnPlayer()
    {
        if (turnPlayer == PlayerColor.White)
        {
            turnPlayer = PlayerColor.Black;
        }
        else
        {
            turnPlayer = PlayerColor.White;
        }
    }
}
