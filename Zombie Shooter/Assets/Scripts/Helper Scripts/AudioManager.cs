using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public static AudioManager instance;
	public AudioClip[] gunSounds;
	public AudioClip meleeSound;
	public AudioSource playerAttackAudioSource;
	public AudioSource zombieAttackAudioSource;
	public AudioSource zombieRiseAudioSource;
	public AudioSource zombieDieAudioSource;
	public AudioClip zombieRiseClip, zombieDieClip;
	public AudioClip[] zombieAttackClips;
	public AudioSource fenceExplosionAudioSource;
	public AudioClip fenceExplosionClip;




	void Awake () {
		MakeSingleton();
	}

	void MakeSingleton()
    {
		if (instance != null)
			Destroy(gameObject);
		else
        {
			instance = this;
			DontDestroyOnLoad(gameObject);
        }
    }


	public void GunSound(int index)
    {
		playerAttackAudioSource.PlayOneShot(gunSounds[index], 1.0f);
    }

	public void MeleeAttackSound()
    {
		playerAttackAudioSource.PlayOneShot(meleeSound, 1.0f);
    }

	public void ZombieRiseSound()
    {
		zombieRiseAudioSource.PlayOneShot(zombieRiseClip, 1.0f);
    }

	public void ZombieDieSound()
    {
		zombieDieAudioSource.PlayOneShot(zombieDieClip, 1.0f);
    }

	public void ZombieAttackSound()
    {
		int index = Random.Range(0, zombieAttackClips.Length);
		zombieAttackAudioSource.PlayOneShot(zombieAttackClips[index], 1.0f);
    }

	public void FenceExplosion()
    {
		fenceExplosionAudioSource.PlayOneShot(fenceExplosionClip, 1.0f);
    }
}
