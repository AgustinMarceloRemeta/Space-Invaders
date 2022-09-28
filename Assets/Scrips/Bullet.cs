using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime), Space.Self);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       this.gameObject.SetActive(false);
    }
}
