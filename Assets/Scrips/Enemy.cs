using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
    }

    public override void Shooting()
    {
        base.Shooting();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null)
        { 
            Destroy(this.gameObject);
            EnemyManager.upgradeListEvent?.Invoke();
        }
    }
}
