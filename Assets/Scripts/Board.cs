using UnityEngine;

public class Board : MonoBehaviour
{
    public float FallTime = 1f;
    public float FasterFallTime = 0.1f;
    public float MoveTime = 0.2f;

    public int StartX = 1, StartY = 1;
    public int Width = 10, Height = 20;

    private bool[,] board;

    void Start()
    {
        board = new bool[Width, Height];
    }

    public void OccupySpot(int x, int y)
    {
        board[x - 1, y - 1] = true;
    }

    public bool IsOccupied(int x, int y)
    {
        return board[x - 1, y - 1];
    }

    public bool IsOnBoard(double x, double y)
    {
        return x >= StartX && x < StartX + Width && y >= StartY && y < StartY + Height;
    }
}
