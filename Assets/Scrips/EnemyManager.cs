using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemys;
    public static Action upgradeListEvent;
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
        RandomShoot();
    }

    void UpgradeList()
    {
        foreach (GameObject item in enemys)  if (item == null) enemys.Remove(item);    
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
