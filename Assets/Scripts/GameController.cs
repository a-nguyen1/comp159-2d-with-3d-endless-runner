using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnDelay;
    [SerializeField] private float enemySpeed;
    private bool gameOver = false;
    private float worldXDist;
    private float worldYDist;

    //PlayerController reference
    private PlayerController player;
    [SerializeField] private float deathPoint; //Set to 5 in the Inspector
    
    // Start is called before the first frame update
    void Start()
    {
        worldYDist = Camera.main.orthographicSize;
        worldXDist = worldYDist * Screen.width / Screen.height;
        StartCoroutine("SpawnEnemies");
        
        //Gets the Player game object
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = player.transform.position;
        if (playerPosition.y < deathPoint)
        {
            Destroy(player.gameObject);
            Debug.Log("PLAYER DIEDED!!!");
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (!gameOver)
        {
            Vector3 enemyPosition = new Vector3(worldXDist, Random.Range(-worldYDist, worldYDist), 0);
            GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
            EnemyController enemyScript = newEnemy.GetComponent<EnemyController>();
            enemyScript.SetSpeed(enemySpeed);
            enemyScript.SetEdge(-worldXDist);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
