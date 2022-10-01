using UnityEngine;

public class Limit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) => collision.gameObject.SetActive(false);
}
