using UnityEngine;

public class Board : MonoBehaviour
{
    public float FallTime = 1f;
    public float FasterFallTime = 0.1f;
    public float MoveTime = 0.2f;

    public int StartX = 1, StartY = 1;
    public int Width = 10, Height = 20;

    public bool IsOnBoard(double x, double y)
    {
        return x >= StartX && x < StartX + Width && y >= StartY && y < StartY + Height;
    }
}
