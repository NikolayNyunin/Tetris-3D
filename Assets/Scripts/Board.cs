using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public float FallTime = 1f;
    public float FasterFallTime = 0.1f;
    public float MoveTime = 0.2f;
    public float LastFallTime = 0.4f;

    public int StartX = 1, StartY = 1;
    public int Width = 10, Height = 20;

    public Text score;
    public int[] Scores = {100, 300, 700, 1500};

    private bool[,] board;
    private List<GameObject> tetrominoes = new List<GameObject>();

    void Start()
    {
        board = new bool[Width, Height];
    }

    public void AddTetromino(GameObject tetromino)
    {
        tetrominoes.Add(tetromino);
    }

    public void OccupySpot(int x, int y)
    {
        board[x - StartX, y - StartY] = true;
    }

    public bool IsOccupied(int x, int y)
    {
        return board[x - StartX, y - StartY];
    }

    public bool IsOnBoard(double x, double y)
    {
        return x >= StartX && x < StartX + Width && y >= StartY && y < StartY + Height;
    }

    private void UpdateBoard()
    {
        board = new bool[Width, Height];

        foreach (GameObject tetromino in tetrominoes)
        {
            foreach (Transform child in tetromino.transform)
            {
                OccupySpot(Convert.ToInt32(child.position.x), Convert.ToInt32(child.position.y));
            }
        }
    }

    public void CheckRows()
    {
        List<int> rowsToDelete = new List<int>();
        for (int y = 0; y < Height; y++)
        {
            bool full = true;
            for (int x = 0; x < Width; x++)
            {
                if (!board[x, y])
                {
                    full = false;
                    break;
                }
            }

            if (full)
                rowsToDelete.Add(y);
        }

        if (rowsToDelete.Count > 0)
            DeleteRows(rowsToDelete);
    }

    public void DeleteRows(List<int> rowsToDelete)
    {
        score.text = (Convert.ToInt32(score.text) + Scores[rowsToDelete.Count - 1]).ToString();

        foreach (int row in rowsToDelete)
        {
            int y = StartY + row;
            Collider[] colliders = Physics.OverlapBox(new Vector3(StartX - 0.5f + Width / 2f, y, 0),
                new Vector3(Width / 2f, 0.1f, 0.1f));
            foreach (Collider col in colliders)
            {
                DestroyImmediate(col.gameObject);
            }
        }

        int j = 0;
        while (j < tetrominoes.Count)
        {
            if (tetrominoes[j].transform.childCount == 0)
            {
                Destroy(tetrominoes[j]);
                tetrominoes.RemoveAt(j);
            }
            else
                j++;
        }

        for (int i = 0; i < rowsToDelete.Count; i++)
        {
            int y = StartY + rowsToDelete[i] - i;
            foreach (GameObject tetromino in tetrominoes)
            {
                foreach (Transform child in tetromino.transform)
                {
                    if (Convert.ToInt32(child.position.y) > y)
                    {
                        child.SetPositionAndRotation(child.position + Vector3.down, child.rotation);
                    }
                }
            }
        }

        UpdateBoard();
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER!");
    }
}
