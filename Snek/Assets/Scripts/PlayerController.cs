using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Directions { Up, Down, Left, Right, Stop }

[Serializable]
public class Direction
{
    public string key;

    public Directions direction;

    public Vector2 vec;
}

public class PlayerController : MonoBehaviour
{
    public Dictionary<Directions, Direction> allDirections = new Dictionary<Directions, Direction>
    {
        [Directions.Up] = new Direction { key = "up", direction = Directions.Up, vec = Vector2.up },
        [Directions.Down] = new Direction { key = "down", direction = Directions.Down, vec = Vector2.down },
        [Directions.Left] = new Direction { key = "left", direction = Directions.Left, vec = Vector2.left },
        [Directions.Right] = new Direction { key = "right", direction = Directions.Right, vec = Vector2.right }
    };

    public float moveSpeed;

    public List<string> keys;

    public Directions currentDir = Directions.Stop;

    public GameObject targetPoint;

    private int targetAmount;

    public bool shuffle;
    public float timeBetweenShuffle;
    private float timer;

    void FixedUpdate()
    {
        Play();

        if (!shuffle) return;

        if (timer <= 0.0f)
        {
            ShuffleInput();

            timer = timeBetweenShuffle;
        }

        timer -= Time.deltaTime;
    }
    
    public virtual void Play()
    {
        CheckInput();

        if (currentDir != Directions.Stop) Move(currentDir);
    }

    void SetDirection(Directions dir)
    {
        bool check = (currentDir == Directions.Stop);

        currentDir = dir;

        targetAmount++;

        if (check) return;

        GameObject target = Instantiate(targetPoint);
        target.GetComponent<TargetPoint>().directionToTake = dir;
        target.GetComponent<TargetPoint>().index = targetAmount;
        target.transform.position = this.transform.position;
    }

    void CheckInput()
    {
        foreach (Directions direction in allDirections.Keys)
        {
            if (Input.GetButton(allDirections[direction].key))
            {
                if (currentDir != direction) SetDirection(direction);
            }
        }
    }

    public void Move(Directions dir)
    {
        transform.Translate(allDirections[dir].vec * Time.deltaTime * moveSpeed);
    }

    void ShuffleInput()
    {
        List<string> shuffledKeys = keys.OrderBy(x => UnityEngine.Random.value).ToList();

        int index = 0;

        foreach (Direction direction in allDirections.Values)
        {
            direction.key = shuffledKeys[index];
            index++;
        }
    }
}
