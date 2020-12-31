using UnityEngine;

public class SpawningNext : MonoBehaviour
{
    private GameObject currentTetromino;

    public GameObject GetTetromino()
    {
        return currentTetromino;
    }

    public void SetTetromino(GameObject tetromino)
    {
        currentTetromino = tetromino;
        currentTetromino.transform.position = transform.position;
    }
}
