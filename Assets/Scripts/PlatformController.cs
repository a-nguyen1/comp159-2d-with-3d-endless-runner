using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform platformDeletePoint;
    private GameObject[] _platforms;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _platforms = GameObject.FindGameObjectsWithTag("Platform");
        
        foreach(GameObject platform in _platforms)
        {
            if (platform.transform.position.x < platformDeletePoint.position.x)
            {
                Destroy(platform);
                Debug.Log("DESTROYED PLATFORM!!!");
            }
        }
    }
}
