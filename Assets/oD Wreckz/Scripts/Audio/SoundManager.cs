using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] shurikenThrowSounds;
    public AudioClip[] bunTakenSounds;
    public AudioClip[] enemyDeathSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShurikenThrowSound()
    {
        if (shurikenThrowSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, shurikenThrowSounds.Length);
            audioSource.PlayOneShot(shurikenThrowSounds[randomIndex]);
        }
    }

    public void PlayBunTakenSound()
    {
        if (bunTakenSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, bunTakenSounds.Length);
            audioSource.PlayOneShot(bunTakenSounds[randomIndex]);
        }
    }

    public void PlayEnemyDeathSounds()
    {
        if (enemyDeathSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeathSounds.Length);
            audioSource.PlayOneShot(enemyDeathSounds[randomIndex]);
        }
    }

}