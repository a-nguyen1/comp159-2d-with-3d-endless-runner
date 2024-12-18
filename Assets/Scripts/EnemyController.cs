using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    private float worldXEdge = float.PositiveInfinity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var enemyTransform = transform;
        Vector3 newPosition = enemyTransform.position;
        newPosition.x -= speed;
        if (newPosition.x < worldXEdge)
        {
            Destroy(gameObject);
        }
        enemyTransform.position = newPosition;
    }
    
    void OnTriggerEnter(Collider other) {
        Health healthScript = other.GetComponent<Health>();
        if (healthScript != null && other.gameObject.CompareTag("Player"))
        {
            healthScript.TakeDamage(1);
            Destroy(gameObject);
        }
    }

    public void SetEdge(float xEdge)
    {
        worldXEdge = xEdge;
    }
    
    public void SetSpeed(float enemySpeed)
    {
        speed = enemySpeed;
    }
}
