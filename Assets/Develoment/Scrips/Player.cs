using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] protected float speed, valueCooldown, projectileSpeed;
    protected float cooldown;
    [SerializeField] Transform triggerPoint;
    Shoot shootManager;
    public bool isDeath;

    public virtual void Start()
    {
        isDeath = false;
        shootManager = Shoot.instance;
    }

    void Update()
    {
        if (!isDeath)
        {
            Movement();
            if (Input.GetKey(KeyCode.Space) && cooldown <= 0) Shooting();
            if (cooldown > 0) cooldown -= Time.deltaTime;
        }
    }

    void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(speed * input * Time.deltaTime, 0));
    }

    public virtual void Shooting()
    {
        AudioManager.instance.InstanceSound("shoot");
     GameObject bullet = shootManager.GetNewBullet();
     bullet.transform.position = triggerPoint.position;
     bullet.transform.rotation = triggerPoint.rotation;
     bullet.SetActive(true);
     bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, projectileSpeed));
     cooldown = valueCooldown;   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Bullet(Clone)")
        {
          if(!isDeath) GameManager.lowLifeEvent?.Invoke();
          collision.gameObject.SetActive(false);
        }
        if (collision.gameObject.GetComponent<Enemy>() != null) GameManager.endGameEvent?.Invoke("Game over");
    }
}
