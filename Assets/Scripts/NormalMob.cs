using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMob : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;


    private float timer = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        if (!audioSource.isPlaying)
        {
            timer += Time.deltaTime;

            if(timer > 3.5f)
            {
                GetComponent<AudioSource>().pitch = Random.Range(.6f, 1.0f);
                GetComponent<AudioSource>().volume = Random.Range(.3f, .5f);
                audioSource.clip = clips[Random.Range(0, clips.Length)];
                audioSource.Play();

                timer = 0;
            }
        }
    }
}
