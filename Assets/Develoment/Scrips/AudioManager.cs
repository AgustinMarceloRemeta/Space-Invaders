using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    [SerializeField]AudioSource shoot, deadEnemy;

    void Start()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void InstanceSound(string type)
    {
        if (type == "shoot") shoot.Play();
        else if (type == "enemy") deadEnemy.Play();
    }

    
}
