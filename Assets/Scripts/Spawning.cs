using System;
using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    private Board board;
    private SpawningNext nextSpawner;

    void Start()
    {
        board = FindObjectOfType<Board>();
        nextSpawner = FindObjectOfType<SpawningNext>();
        nextSpawner.SetTetromino(GenerateTetromino());
        NewTetromino();
    }

    public GameObject NewTetromino()
    {
        GameObject tetromino = nextSpawner.GetTetromino();

        nextSpawner.SetTetromino(GenerateTetromino());

        tetromino.transform.position = transform.position;
        tetromino.GetComponent<Tetromino>().enabled = true;
        board.AddTetromino(tetromino);

        foreach (Transform child in tetromino.transform)
        {
            int x = Convert.ToInt32(child.position.x), y = Convert.ToInt32(child.position.y);
            if (board.IsOccupied(x, y))
            {
                Destroy(tetromino);
                board.GameOver();
                break;
            }
        }

        return tetromino;
    }

    private GameObject GenerateTetromino()
    {
        GameObject tetromino = Tetrominoes[UnityEngine.Random.Range(0, Tetrominoes.Length)];
        tetromino = Instantiate(tetromino, transform.position, tetromino.transform.rotation);
        tetromino.GetComponent<Tetromino>().enabled = false;
        return tetromino;
    }
}
