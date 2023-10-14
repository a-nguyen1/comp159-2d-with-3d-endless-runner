using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnDelay;
    [SerializeField] private float enemySpeed;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject lose;
    [SerializeField] private Button restartButton;
    private bool gameOver = false;
    private float worldXDist;
    private float worldYDist;

    //PlayerController reference
    private PlayerController player;
    [SerializeField] private float deathPoint; //Set to 5 in the Inspector
    
    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(gameOver);
        lose.SetActive(gameOver);
        
        worldYDist = Camera.main.orthographicSize;
        worldXDist = worldYDist * Screen.width / Screen.height;
        StartCoroutine("SpawnEnemies");
        
        //Gets the Player game object
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            var playerPosition = player.transform.position;
            if (playerPosition.y < deathPoint)
            {
                gameOver = true;
                player.GetComponent<Health>().SetInvincibility(false);
                player.GetComponent<Health>().TakeDamage(float.PositiveInfinity);
                Destroy(player.gameObject);
                Debug.Log("PLAYER DIED!!!");
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        gameOver = true;
        DestroyObjectsWithTag("Enemy");
        DestroyObjectsWithTag("Platform");
        Debug.Log("Game Over triggered");

        background.SetActive(gameOver);
        lose.SetActive(gameOver);
    }

    private static void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in objects)
        {
            Destroy(obj);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (!gameOver)
        {
            Vector3 enemyPosition = new Vector3(worldXDist, Random.Range(-worldYDist, worldYDist), 1);
            GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
            EnemyController enemyScript = newEnemy.GetComponent<EnemyController>();
            enemyScript.SetSpeed(enemySpeed);
            enemyScript.SetEdge(-worldXDist);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
