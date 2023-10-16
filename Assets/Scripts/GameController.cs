using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private float enemySpawnDelay;
    [SerializeField] private float enemySpeed;
    [SerializeField] private float enemySpeedScale = 0.005f;
    [SerializeField] private GameObject background;
    [SerializeField] private GameObject lose;
    [SerializeField] private Button restartButton;
    [SerializeField] private Text finalScore;
    private bool gameOver = false;
    private float worldXDist;
    private float worldYDist;

    //PlayerController reference
    private PlayerController player;
    [SerializeField] private float deathPoint; //Set to 5 in the Inspector
    
    //Player Score
    private int _playerScore;
    [SerializeField] private TextMeshProUGUI scoreCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(gameOver);
        lose.SetActive(gameOver);
        finalScore.enabled = gameOver;

        restartButton.onClick.AddListener(RestartButton);
        
        //Gets the Player game object
        player = FindObjectOfType<PlayerController>();
        
        worldYDist = Camera.main.orthographicSize;
        worldXDist = worldYDist * Screen.width / Screen.height;
        StartCoroutine("SpawnEnemies");
        
        //Initialize playerScore
        _playerScore = 0;

        finalScore.text = "Final Score: " + _playerScore.ToString();
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

        Debug.Log("PLAYER FINAL SCORE IS: " + _playerScore);
        
        background.SetActive(gameOver);
        lose.SetActive(gameOver);
        
        scoreCounter.enabled = false;

        finalScore.enabled = gameOver;
        
    }

    public void RestartButton()
    {
        background.SetActive(gameOver);
        lose.SetActive(gameOver);
        SceneManager.LoadScene("GameScene");
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
            var playerY = player.transform.position.y;
            float minimumY = playerY - worldYDist;
            float maximumY = playerY + worldYDist;
            Vector3 enemyPosition = new Vector3(worldXDist, Random.Range(minimumY, maximumY), 1);
            GameObject newEnemy = Instantiate(enemy, enemyPosition, Quaternion.identity);
            EnemyController enemyScript = newEnemy.GetComponent<EnemyController>();
            enemyScript.SetSpeed(enemySpeed);
            enemySpeed += enemySpeedScale;
            enemyScript.SetEdge(-worldXDist);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }

    public void IncrementScore()
    {
        _playerScore++;
        scoreCounter.text = _playerScore.ToString();
        finalScore.text = "Final Score: " + _playerScore.ToString();
    }
}
