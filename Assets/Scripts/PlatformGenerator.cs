using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;
    [SerializeField] private int secondsToWait;
    private float platformWidth;
    
    

    // Start is called before the first frame update
    void Start()
    {
       //Getting platform width
       platformWidth = thePlatform.GetComponent<BoxCollider>().size.x;
       
    }

    private void FixedUpdate()
    {
        if (generationPoint != null) //check if generationPoint exists
        {
            StartCoroutine(CaculateGenTime());
        
       
            if (transform.position.x < generationPoint.position.x)
            {
                //Moving transform position to repeat making platforms
                transform.position = new Vector3(transform.position.x + platformWidth + distanceBetween,
                    transform.position.y, transform.position.z);
                //Generating the platform
                Instantiate(thePlatform, transform.position, Quaternion.identity);
            
            }
        }
    }

    private void MoveGenPoint()
    {
        if (generationPoint != null)//Check if generationPoint exists
        {
            generationPoint.transform.position = new Vector3(generationPoint.position.x + 1, generationPoint.position.y,
                generationPoint.position.z);
        }
        
    }

    IEnumerator CaculateGenTime()
    {
        yield return new WaitForSeconds(secondsToWait);
        MoveGenPoint();
    }
}
