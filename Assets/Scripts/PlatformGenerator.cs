using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;
    
    // Start is called before the first frame update
    void Start()
    {
       //Getting platform width
       platformWidth = thePlatform.GetComponent<BoxCollider>().size.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
