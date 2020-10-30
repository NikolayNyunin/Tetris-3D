using System;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    private Spawning spawner;
    private Board board;

    private double previousFallTime, previousMoveTime;
    private bool fallingFast = false;
    private bool movable = true;

    void Start()
    {
        spawner = FindObjectOfType<Spawning>();
        board = FindObjectOfType<Board>();

        previousFallTime = Time.time;
        previousMoveTime = Time.time;
    }

    void Update()
    {
        if (!movable)
            return;

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
                movable = false;
                spawner.NewTetromino();
            }
        }
    }

    private void ProcessInput()
    {
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
            double x = Math.Round(child.transform.position.x), y = Math.Round(child.transform.position.y);
            if (!board.IsOnBoard(x, y))
                return false;
        }

        return true;
    }
}
