namespace DragonChess.CommonLibrary;

public enum PlayerColor : byte
{
    White,
    Black,
    None
}

public enum MoveType : byte
{
    MoveCapture,
    MoveOnly,
    CaptureOnly
}