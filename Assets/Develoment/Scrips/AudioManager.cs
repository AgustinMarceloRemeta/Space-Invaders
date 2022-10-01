using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public static Action<string> instanceSound;
    [SerializeField]AudioSource shoot, deadEnemy;

    void Start()
    {
        if (instance != null && instance != this) Destroy(this.gameObject);
        else instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void InstanceSound(string type)
    {
        if (type == "shoot") shoot.Play();
        else if (type == "enemy") deadEnemy.Play();
    }

    private void OnEnable() =>  instanceSound += InstanceSound;
   
    private void OnDisable() => instanceSound -= InstanceSound;
    
}
