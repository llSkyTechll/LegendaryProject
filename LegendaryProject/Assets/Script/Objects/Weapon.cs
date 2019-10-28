using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Weapon : Equipment { 

    public int minDamage;
    public int maxDamage;
    protected float range;
    public AudioClip SwishSound;
    public AudioClip HitSound;
    private AudioSource soundplayer;

    // Use this for initialization
    void Start () {
        rarity = (Rarity)randomRarity.Next(0,5);
        soundplayer = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Swish()
    {
        soundplayer.pitch = Random.Range(0.8f, 1.2f);
        soundplayer.volume = Random.Range(0.8f, 1.1f);
        soundplayer.PlayOneShot(SwishSound);
    }

    public void Hit()
    {
        soundplayer.pitch = Random.Range(0.8f, 1.2f);
        soundplayer.volume = Random.Range(0.8f, 1.1f);
        soundplayer.PlayOneShot(HitSound);
    }
}
