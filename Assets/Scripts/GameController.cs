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
    // Start is called before the first frame update
    void Start()
    {
        worldYDist = Camera.main.orthographicSize;
        worldXDist = worldYDist * Screen.width / Screen.height;
        StartCoroutine("SpawnEnemies");
    }

    // Update is called once per frame
    void Update()
    {
        
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
