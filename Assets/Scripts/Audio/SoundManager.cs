using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] themeMusicSounds;
    public AudioClip[] menuMusicSounds;
    public AudioClip[] shurikenThrowSounds;
    public AudioClip[] bunTakenSounds;
    public AudioClip[] enemyDeathSounds;
    public AudioClip[] hitByShurikenSounds;
    public AudioClip[] girlAttackSound;
    public AudioClip[] girlHitSound;
    public AudioClip[] dropBoxSound;
    public AudioClip[] liftBoxSound;

    public float themeMusicVolume = 1.0f;
    public float menuMusicVolume = 1.0f;
    public float shurikenThrowVolume = 1.0f;
    public float bunTakenVolume = 1.0f;
    public float enemyDeathVolume = 1.0f;
    public float hitByShurikenVolume = 1.0f;
    public float girlAttackVolume = 1.0f;
    public float girlHitVolume = 1.0f;
    public float dropBoxVolume = 1.0f;
    public float liftBoxVolume = 1.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PlayThemeMusicSound();
        PlayMenuMusicSound();
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

    public void PlayMenuMusicSound()
    {
        if (menuMusicSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, menuMusicSounds.Length);
            audioSource.clip = menuMusicSounds[randomIndex];
            audioSource.volume = menuMusicVolume;
            audioSource.loop = true;
            audioSource.Play();
            Debug.Log("Play menu theme");
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

    public void PlayGirlHitSound()
    {
        if (girlHitSound.Length > 0)
        {
            int randomIndex = Random.Range(0, girlHitSound.Length);
            audioSource.PlayOneShot(girlHitSound[randomIndex], girlHitVolume);
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

    public void PlayDropBoxSounds()
    {
        if (dropBoxSound.Length > 0)
        {
            int randomIndex = Random.Range(0, dropBoxSound.Length);
            audioSource.PlayOneShot(dropBoxSound[randomIndex], dropBoxVolume);
        }
    }

    public void PlayLiftBoxSounds()
    {
        if (liftBoxSound.Length > 0)
        {
            int randomIndex = Random.Range(0, liftBoxSound.Length);
            audioSource.PlayOneShot(liftBoxSound[randomIndex], liftBoxVolume);
        }
    }
}