using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action lowLifeEvent, deathEvent, addScoreEvent;
    [SerializeField] int life;
    [SerializeField] float timeToDeath; 
    int score;
    GameObject player, playerActive;
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] Text scoreText, lifeText;
    
    void Start()
    {
        score = 0;
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
        if (life > 0)
        {
            StartCoroutine(DestroyPlayer(timeToDeath));
        }
        else Death();
    }

    void InstanciatePlayer()
    {
      playerActive = Instantiate(player, spawnPosition, Quaternion.identity);
    }

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

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnEnable()
    {
        lowLifeEvent += Lowlife;
        deathEvent += Death;
        addScoreEvent += AddScore;
    }
    private void OnDisable()
    {
        lowLifeEvent -= Lowlife;
        deathEvent -= Death;
        addScoreEvent -= AddScore;
    }
}
