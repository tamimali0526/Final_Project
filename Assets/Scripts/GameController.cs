using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private AudioSource audioSource;
    public GameObject Background;

    public AudioClip winMusic;
    public AudioClip loseMusic;
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public float timer;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text timerText;
    public Text winText;

    private bool gameOver;
    private bool restart;
    private int score;
    private bool winGame;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        UpdateTime();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        UpdateTime();
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'W' for Restart";
                restart = true;
                break;
            }
            if (winGame)
            {
                restartText.text = "Press 'W' for Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {
            audioSource.PlayOneShot(winMusic);
            winText.text = "Game created by Tamim Ali";
            Background.GetComponent<BGScroller>().scrollSpeed = -20;
           
            gameOver = true;
            restart = true;
            winGame = true;
        }
        
    }
    void UpdateTime()
    {
        timer = this.gameObject.GetComponent<Timer>().timeRemaining;
        timerText.text = "Time: " + timer;
        if (timer <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.text = "Game created by Tamim Ali!";
        gameOver = true;
        audioSource.PlayOneShot(loseMusic);
    }
}