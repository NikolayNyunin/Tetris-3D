using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    public Vector3 RotationPoint;

    private Spawning spawner;
    private Board board;

    private double previousFallTime, previousMoveTime;
    private bool fallingFast = false;

    void Start()
    {
        spawner = FindObjectOfType<Spawning>();
        board = FindObjectOfType<Board>();

        previousFallTime = Time.time;
        previousMoveTime = Time.time;
    }

    void Update()
    {
        ProcessInput();

        double time = fallingFast ? board.FasterFallTime : board.FallTime;

        if (Time.time - previousFallTime >= time)
        {
            transform.SetPositionAndRotation(transform.position + Vector3.down, transform.rotation);
        
            if (ValidMove())
            {
                previousFallTime = Time.time;
            }
            else
            {
                transform.SetPositionAndRotation(transform.position + Vector3.up, transform.rotation);

                foreach (Transform child in transform)
                    board.OccupySpot(Convert.ToInt32(child.position.x), Convert.ToInt32(child.position.y));

                board.CheckRows();
                spawner.NewTetromino();
                enabled = false;
            }
        }
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
            Rotate();

        if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
            fallingFast = !fallingFast;

        if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            transform.SetPositionAndRotation(transform.position + Vector3.left, transform.rotation);
            if (!ValidMove())
                transform.SetPositionAndRotation(transform.position + Vector3.right, transform.rotation);
            else
                previousMoveTime = Time.time;
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            if (Time.time - previousMoveTime >= board.MoveTime)
            {
                transform.SetPositionAndRotation(transform.position + Vector3.left, transform.rotation);
                if (!ValidMove())
                    transform.SetPositionAndRotation(transform.position + Vector3.right, transform.rotation);
                else
                    previousMoveTime = Time.time;
            }
        }

        if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            transform.SetPositionAndRotation(transform.position + Vector3.right, transform.rotation);
            if (!ValidMove())
                transform.SetPositionAndRotation(transform.position + Vector3.left, transform.rotation);
            else
                previousMoveTime = Time.time;
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
            if (Time.time - previousMoveTime >= board.MoveTime)
            {
                transform.SetPositionAndRotation(transform.position + Vector3.right, transform.rotation);
                if (!ValidMove())
                    transform.SetPositionAndRotation(transform.position + Vector3.left, transform.rotation);
                else
                    previousMoveTime = Time.time;
            }
        }
    }

    private bool ValidMove()
    {
        foreach (Transform child in transform)
        {
            int x = Convert.ToInt32(child.position.x), y = Convert.ToInt32(child.position.y);
            if (!board.IsOnBoard(x, y))
                return false;
            if (board.IsOccupied(x, y))
                return false;
        }

        return true;
    }

    private void Rotate()
    {
        Vector3 absoluteRotationPoint = transform.TransformPoint(RotationPoint);
        transform.RotateAround(absoluteRotationPoint, new Vector3 (0, 0, 1), -90);

        if (!ValidMove())
            transform.RotateAround(absoluteRotationPoint, new Vector3(0, 0, 1), 90);
    }
}
