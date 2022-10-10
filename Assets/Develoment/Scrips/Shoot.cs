using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public static Shoot instance{ get; private set; }
    GameObject bullet;
    [SerializeField] int amountToPool;
    public List<GameObject> bullets;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
    }

    void Start() => InstantiateBullets();

    private void InstantiateBullets()
    {
        bullet = Resources.Load<GameObject>("Prefabs/Bullet");
        GameObject newObject;
        for (int i = 0; i < amountToPool; i++)
        {
            newObject = Instantiate(bullet);
            newObject.SetActive(false);
            bullets.Add(newObject);
        }
    }

    public GameObject GetNewBullet()
    {
        foreach (GameObject item in bullets) if (!item.activeInHierarchy) return item ;  
        return null;
    }
}
