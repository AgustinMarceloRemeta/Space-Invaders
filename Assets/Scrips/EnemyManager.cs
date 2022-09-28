using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemys;
    public static Action<GameObject> upgradeListEvent;
    [SerializeField] float cooldownShoot;
    float cooldownReal;
    void Start()
    {
        foreach  (Enemy item in FindObjectsOfType<Enemy>())
        {
            enemys.Add(item.gameObject);
        } 
    }

    void Update()
    {
       if(enemys.Any()) RandomShoot();
    }

    void UpgradeList(GameObject enemy)
    {
        enemys.Remove(enemy);     
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
        if (cooldownReal > 0) cooldownReal -= Time.deltaTime;
    }

    private void OnEnable()
    {
        upgradeListEvent += UpgradeList;
    }
    private void OnDisable()
    {
        upgradeListEvent -= UpgradeList;
    }
}
