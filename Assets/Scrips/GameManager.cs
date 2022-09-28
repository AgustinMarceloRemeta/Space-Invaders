using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action lowLifeEvent, deathEvent;
    [SerializeField] int life;
    [SerializeField] float timeToDeath;
    GameObject player, playerActive;
    [SerializeField] Vector3 spawnPosition;
    
    void Start()
    {
        player = Resources.Load<GameObject>("Prefabs/Player");
        InstanciatePlayer();
    }


    void Update()
    {
        
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
    }

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void OnEnable()
    {
        lowLifeEvent += Lowlife;
        deathEvent += Death;
    }
    private void OnDisable()
    {
        lowLifeEvent -= Lowlife;
        deathEvent -= Death;
    }
}
