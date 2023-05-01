using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] themeMusicSounds;
    public AudioClip[] shurikenThrowSounds;
    public AudioClip[] bunTakenSounds;
    public AudioClip[] enemyDeathSounds;
    public AudioClip[] hitByShurikenSounds;
    public AudioClip[] girlAttackSound;

    public float themeMusicVolume = 1.0f;
    public float shurikenThrowVolume = 1.0f;
    public float bunTakenVolume = 1.0f;
    public float enemyDeathVolume = 1.0f;
    public float hitByShurikenVolume = 1.0f;
    public float girlAttackVolume = 1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayThemeMusicSound();
    }

    public void PlayThemeMusicSound()
    {
        if (themeMusicSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, themeMusicSounds.Length);
            audioSource.clip = themeMusicSounds[randomIndex];
            audioSource.volume = themeMusicVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play theme");
        }
    }

    public void PlayShurikenThrowSound()
    {
        if (shurikenThrowSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, shurikenThrowSounds.Length);
            audioSource.PlayOneShot(shurikenThrowSounds[randomIndex], shurikenThrowVolume);
        }
    }

    public void PlayBunTakenSound()
    {
        if (bunTakenSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, bunTakenSounds.Length);
            audioSource.PlayOneShot(bunTakenSounds[randomIndex], bunTakenVolume);
        }
    }

    public void PlayEnemyDeathSounds()
    {
        if (enemyDeathSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeathSounds.Length);
            audioSource.PlayOneShot(enemyDeathSounds[randomIndex], enemyDeathVolume);
        }
    }

    public void PlayGirlAttackSounds()
    {
        if (girlAttackSound.Length > 0)
        {
            int randomIndex = Random.Range(0, girlAttackSound.Length);
            audioSource.PlayOneShot(girlAttackSound[randomIndex], girlAttackVolume);
        }
    }

    public void PlayHitByShuriken()
    {
        if (hitByShurikenSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, hitByShurikenSounds.Length);
            audioSource.PlayOneShot(hitByShurikenSounds[randomIndex], hitByShurikenVolume);
        }
    }
}