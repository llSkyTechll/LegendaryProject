using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    // Ajoute des sons d'ambiance autour du joueur
    public AudioClip SurroundSound;
    private AudioSource audioplayer;
    private GameObject player;
    private float timer;

	// Use this for initialization
	void Start () {
        audioplayer = GetComponent<AudioSource>();
        audioplayer.spatialBlend = 1;
        player = GameObject.FindGameObjectWithTag("Player");
        timer = Random.Range(10.1f,20.4f);
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
		if(timer<0)
        {
            Vector3 position = player.transform.position;
            transform.position = new Vector3(position.x+Random.Range(-300,300), position.y + Random.Range(-300, 300), position.z + Random.Range(-300, 300));
            audioplayer.pitch = Random.Range(0.8f, 1.2f);
            audioplayer.volume = Random.Range(0.8f, 1.1f);
            audioplayer.panStereo = Random.Range(0.8f, 1.2f);
            audioplayer.PlayOneShot(SurroundSound);
            timer = Random.Range(10.1f, 20.4f);
        }
	}
}
