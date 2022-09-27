using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    void Start()
    {
        
    }


    void Update()
    {
        Movement();
    }
    void Movement()
    {
        float input = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(speed * input * Time.deltaTime, 0));
    }
}
