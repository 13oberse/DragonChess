namespace DragonChess.CommonLibrary;

public enum PlayerColor : byte
{
    White,
    Black
}

public enum MoveType : byte
{
    MoveCapture,
    MoveOnly,
    CaptureOnly
}