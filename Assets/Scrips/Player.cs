using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed, valueCooldown;
    float cooldown;
    [SerializeField] Transform triggerPoint;
    Shoot shootManager;

    void Start()
    {
        shootManager = FindObjectOfType<Shoot>();
    }


    void Update()
    {
        Movement();
        Shooting();
    }
    void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(speed * input * Time.deltaTime, 0));
    }

    void Shooting()
    {
        if (Input.GetKey(KeyCode.Space) && cooldown<= 0)
        {
            GameObject bullet = shootManager.GetNewBullet();
            bullet.transform.position = triggerPoint.position;
            bullet.transform.rotation = triggerPoint.rotation;
            bullet.SetActive(true);
            cooldown = valueCooldown;
        }
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }
}
