using UnityEngine;

public class Enemy : Player
{
    [SerializeField] float movDown, initialCooldown, upSpeed, timeToUpSpeed;

    public override void Start()
    {
        base.Start();
        InvokeRepeating("UpSpeed", timeToUpSpeed, timeToUpSpeed);
    }

    void Update()
    {
        if (initialCooldown > 0) initialCooldown -= Time.deltaTime;
        if (initialCooldown <= 0) Movement();
    }

    public override void Shooting() => base.Shooting();
    
    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.name == "Bullet(Clone)" && collision.gameObject.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            AudioManager.instance.InstanceSound("shoot");
            GameManager.addScoreEvent?.Invoke();
            EnemyManager.upgradeListEvent?.Invoke(this.gameObject);
            collision.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
        else if (collision.gameObject.name == "Limit")
        {
            speed *= -1;
            this.transform.position = this.transform.position - new Vector3(0, movDown);
        }
    }

    void Movement() => transform.Translate(new Vector2(speed*Time.deltaTime, 0));
      
    void UpSpeed()
    {
        if (speed > 0) speed += upSpeed;
        else speed -= upSpeed;
    }
}
