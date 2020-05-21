using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    public GameObject powerUpPrefab;

    public Transform parent;

    public float delay;
    private float timer;

    public Vector2 verticalMinMax;
    public Vector2 horizontalMinMax;

    void Update()
    {
        if (timer <= 0.0f)
        {
            SpawnPowerUp();

            timer = delay;
        }

        timer -= Time.deltaTime;
    }

    void SpawnPowerUp()
    {
        GameObject powPrefab = Instantiate(powerUpPrefab);
        powPrefab.transform.SetParent(parent);

        powPrefab.transform.position = new Vector2(Random.Range(horizontalMinMax.x, horizontalMinMax.y), Random.Range(verticalMinMax.x, verticalMinMax.y));
    }
}
