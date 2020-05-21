using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ChildController : PlayerController
{
    public PlayerController mainSnek;

    public Directions currentDirFollowed;
    
    public float offset;

    private GameObject target;

    void FixedUpdate()
    {
        Play();

        if (target != null)
        {
            Debug.Log("yes");

            if (transform.position == target.transform.position)
            {
                currentDirFollowed = target.GetComponent<TargetPoint>().directionToTake;

                target = null;
            }
        }
    }
    
    public override void Play()
    {
        if (mainSnek.currentDir != Directions.Stop) Move(currentDirFollowed);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.gameObject.tag == "Target")
        {
            target = other.gameObject;
        }
    }
}
