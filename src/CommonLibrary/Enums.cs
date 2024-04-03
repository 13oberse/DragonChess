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
public enum CheckState : byte
{
    None,
    Check,
    Checkmate
}