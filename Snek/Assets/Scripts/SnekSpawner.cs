using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SnekSpawner : MonoBehaviour
{
    public ScoreContainer scoreContainer;

    public GameObject prefab;

    public Transform parent;

    public GameObject lastSnek;

    public float offset;

    public PlayerController mainController;

    public GameObject scoreDisplayer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PowerUp")
        {
            InstantiateSnek();

            scoreContainer.score += 100;

            scoreDisplayer.GetComponent<Text>().text = scoreContainer.score.ToString();

            Destroy(other.gameObject);
        }
    }
    
    void InstantiateSnek()
    {
        GameObject _prefab = Instantiate(prefab);
        _prefab.transform.SetParent(parent);

        ChildController lastSnekController = lastSnek.GetComponent<ChildController>();

        Vector3 pos = mainController.allDirections[lastSnekController.currentDirFollowed].vec;

        _prefab.transform.position = lastSnek.transform.position + (pos * offset);

        _prefab.GetComponent<ChildController>().currentDirFollowed = lastSnekController.currentDirFollowed;

        _prefab.GetComponent<ChildController>().mainSnek = mainController;
        
        lastSnek = _prefab;
    }
}
