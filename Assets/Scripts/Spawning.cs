using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    private Board board;

    void Start()
    {
        board = FindObjectOfType<Board>();
        board.AddTetromino(NewTetromino());
    }

    public GameObject NewTetromino()
    {
        GameObject tetromino = Tetrominoes[Random.Range(0, Tetrominoes.Length)];
        return Instantiate(tetromino, transform.position, tetromino.transform.rotation);
    }
}
