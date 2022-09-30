using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Player
{
    [SerializeField] float movDown, initCooldown, upSpeed, timeToUpSpeed;
    public override void Start()
    {
        base.Start();
        StartCoroutine(UpSpeed(timeToUpSpeed));
    }

    void Update()
    {
        if (initCooldown > 0) initCooldown -= Time.deltaTime;
        if (initCooldown <= 0) Movement();
    }

    public override void Shooting()
    {
        base.Shooting();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.name == "Bullet(Clone)" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            GameManager.addScoreEvent?.Invoke();
            EnemyManager.upgradeListEvent?.Invoke(this.gameObject);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        if (collision.gameObject.name == "Limit")
        {
            speed *= -1;
            this.transform.position = this.transform.position - new Vector3(0, movDown);
        }
    }

    void Movement()
    {
        transform.Translate(new Vector2(speed*Time.deltaTime, 0));
    }

    IEnumerator UpSpeed(float time)
    {
        yield return new WaitForSeconds(time);
        if (speed > 0) speed += upSpeed;
        else speed -= upSpeed;
        StartCoroutine(UpSpeed(time));
    }

}
