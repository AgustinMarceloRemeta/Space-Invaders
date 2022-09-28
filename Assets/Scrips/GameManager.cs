using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Action deathEvent;
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

    void Death()
    {
        if (life > 0) 
        {
            StartCoroutine(DestroyPlayer(timeToDeath));
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    private void OnEnable()
    {
        deathEvent += Death;
    }
    private void OnDisable()
    {
        deathEvent -= Death;
    }
}
