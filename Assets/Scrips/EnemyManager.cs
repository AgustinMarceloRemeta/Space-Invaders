using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] List<GameObject> enemys;
    [SerializeField] List<Vector2> positions;
    public static Action<GameObject> upgradeListEvent;
    [SerializeField] float cooldownShoot;
    float cooldownReal;
    GameObject enemy;
    void Start()
    {
        instanciateEnemys();
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

    void instanciateEnemys()
    {
        enemy = Resources.Load<GameObject>("Prefabs/Enemy");
        GameObject newObject;

        for (int i = 0; i < positions.Count; i++)
        {
            newObject = Instantiate(enemy);
            newObject.transform.position = positions[i];
            enemys.Add(newObject);
        }
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
