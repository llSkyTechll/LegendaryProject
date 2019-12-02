using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {

    private AudioSource audioplayer;
    public AudioClip Music;

	// Use this for initialization
	void Start () {
        audioplayer = GetComponent<AudioSource>();
        audioplayer.clip = Music;
        audioplayer.loop = true;

    }
	
	// Update is called once per frame
	void Update () {
		if(!audioplayer.isPlaying && Music != null)
        {
            audioplayer.Play();
        }
	}
}
