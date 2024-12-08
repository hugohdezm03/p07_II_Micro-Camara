using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    private AudioSource zombieSound;
    // Start is called before the first frame update
    void Start()
    {
        CollisionNotifier.onEggCollision += PlaySound;
        zombieSound = GameObject.FindWithTag("zombie_sound").GetComponent<AudioSource>();
    }

    void PlaySound()
    {
        zombieSound.Play();
    }
}
