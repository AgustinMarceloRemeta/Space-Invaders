using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemys;
    [SerializeField] List<Vector2> positions;
    public static Action<GameObject> upgradeListEvent;
    [SerializeField] float cooldownShoot, timeToInit;
    [SerializeField] int minSpawn, maxSpawn;
    float cooldownReal;
    GameObject enemy;
    public List<int> spawns;

    void Start() => Invoke("instanciateEnemys", timeToInit);
 
    void Update()
    {
        if (enemys.Any()) RandomShoot();
    }

    void UpgradeList(GameObject enemy)
    {
        enemys.Remove(enemy);
        if (!enemys.Any()) GameManager.endGameEvent?.Invoke("You Win");
    }

    void RandomShoot()
    {
        if (cooldownReal <= 0)
        {
            int randomEnemy = UnityEngine.Random.Range(0, enemys.Count);
            GameObject enemySelected = enemys[randomEnemy];
            if (enemySelected != null) enemySelected.GetComponent<Enemy>().Shooting();
            cooldownReal = cooldownShoot;
        }
        else if (cooldownReal > 0) cooldownReal -= Time.deltaTime;
    }

    void instanciateEnemys()
    {  
        int amountEnemy = UnityEngine.Random.Range(minSpawn, maxSpawn);
        for (int i = 0; i < amountEnemy;)
        {
            int random = UnityEngine.Random.Range(0, positions.Count);
           
            if (!spawns.Contains(random))
            {
                spawns.Add(random);
                i++;
            }
        }
        enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        GameObject newObject;
        for (int i = 0; i < spawns.Count; i++)
        {
            newObject = Instantiate(enemy);
            newObject.transform.position = positions[spawns[i]];
            enemys.Add(newObject);
        }
    }

    private void OnEnable() => upgradeListEvent += UpgradeList;

    private void OnDisable() => upgradeListEvent -= UpgradeList;   
}
