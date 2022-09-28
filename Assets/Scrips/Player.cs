using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, valueCooldown;
    [SerializeField] protected float cooldown;
    [SerializeField] Transform triggerPoint;
    Shoot shootManager;
    public bool isDeath;

    public virtual void Start()
    {
        isDeath = false;
        shootManager = FindObjectOfType<Shoot>();
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
     GameObject bullet = shootManager.GetNewBullet();
     bullet.transform.position = triggerPoint.position;
     bullet.transform.rotation = triggerPoint.rotation;
     bullet.SetActive(true);
     cooldown = valueCooldown;   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Bullet>() != null) GameManager.lowLifeEvent?.Invoke();
        if (collision.gameObject.GetComponent<Enemy>() != null) GameManager.deathEvent?.Invoke(); ;
    }
}
