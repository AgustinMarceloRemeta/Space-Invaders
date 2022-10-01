using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action lowLifeEvent, addScoreEvent;
    public static Action<string> endGameEvent;
    [SerializeField] int life;
    [SerializeField] float timeToDeath, resetTime; 
    int score;
    GameObject player, playerActive;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Text scoreText, lifeText, deadOrWinText;
     
    void Start()
    {
        score = PlayerPrefs.GetInt("Score",0);
        player = Resources.Load<GameObject>("Prefabs/Player");
        InstanciatePlayer();
        UpdateUi();
    }

    void AddScore()
    {
        score++;
        UpdateUi();
    }

    private void UpdateUi()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "x" + life;
    }

    void Lowlife()
    {
        if (life > 0) StartCoroutine(DestroyPlayer(timeToDeath)); 
        else EndGame("Game over");
    }
     
    void InstanciatePlayer() => playerActive = Instantiate(player, spawnPosition, Quaternion.identity);
    
    IEnumerator DestroyPlayer(float time)
    {
        playerActive.GetComponent<SpriteRenderer>().color = Color.red;
        playerActive.GetComponent<Player>().isDeath = true;
        yield return new WaitForSeconds(time);
        Destroy(playerActive);
        InstanciatePlayer();
        life--;
        UpdateUi();
    }

    void EndGame(string endText)
    {
        deadOrWinText.text = endText;
        foreach (Enemy item in FindObjectsOfType<Enemy>()) item.enabled = false;
        foreach (Player item in FindObjectsOfType<Player>()) item.isDeath = true;
        FindObjectOfType<EnemyManager>().enabled = false;
        if (endText == "Game over") PlayerPrefs.SetInt("Score", 0);
        else PlayerPrefs.SetInt("Score", score);
        StartCoroutine(Reset(resetTime));
    }

    private IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToMenu() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    private void OnEnable()
    {
        lowLifeEvent += Lowlife;
        endGameEvent = EndGame;
        addScoreEvent += AddScore;
    }

    private void OnDisable()
    {
        lowLifeEvent -= Lowlife;
        endGameEvent -= EndGame;
        addScoreEvent -= AddScore;
    }
}
