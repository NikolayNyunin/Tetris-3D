using UnityEngine;

public class Spawning : MonoBehaviour
{
    public GameObject[] Tetrominoes;

    void Start()
    {
        NewTetromino();
    }

    public void NewTetromino()
    {
        GameObject tetromino = Tetrominoes[Random.Range(0, Tetrominoes.Length)];
        Instantiate(tetromino, transform.position, tetromino.transform.rotation);
    }
}
