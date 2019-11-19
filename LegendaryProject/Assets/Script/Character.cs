using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public abstract class Character : MonoBehaviour, Damageable
{
    protected Health health;
    protected int MaxLife = 1;
    protected AudioSource soundplayer;
    public AudioClip step;
    protected bool isDead = false;

    public void TakeDamage(int damage)
    {
        OnDamage(damage);
    }

    public abstract void OnDamage(int damage);

    public abstract void Die();

    protected void SetLife(int newMaxLife)
    {
        MaxLife = newMaxLife;
    }

    public abstract void Footsteps();
    protected void PlayRepeatingSound(AudioClip clip, float pitchOffset = 0, float volumeOffset = 0)
    {
        if (!soundplayer.isPlaying)
        {
            soundplayer.pitch = UnityEngine.Random.Range(0.8f + pitchOffset, 1.2f + pitchOffset);
            soundplayer.volume = UnityEngine.Random.Range(0.8f + volumeOffset, 1.1f + volumeOffset);
            soundplayer.PlayOneShot(clip);
        }
    }

    protected void PlaySound(AudioClip clip, float pitchOffset = 0, float volumeOffset = 0, float timeOffset = 0)
    {
        soundplayer.Stop();
        soundplayer.pitch = UnityEngine.Random.Range(0.8f + pitchOffset, 1.2f + pitchOffset);
        soundplayer.volume = UnityEngine.Random.Range(0.8f + volumeOffset, 1.1f + volumeOffset);
        StartCoroutine(PlaySoundDelay(clip, timeOffset));
    }

    private IEnumerator PlaySoundDelay(AudioClip clip, float timeOffset)
    {
        yield return new WaitForSeconds(timeOffset);
        soundplayer.PlayOneShot(clip);
    }
}