using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    private static BackgroundMusicManager instance;
    private AudioSource backgroundMusicSource;
    [SerializeField] AudioClip backgroundMusicClip;
    [SerializeField] float volume = 0.2f;

    void Awake()
    {
        // Cek apakah sudah ada instance dari manager ini
        if (instance == null)
        {
            instance = this;
            // Agar GameObject ini tidak dihancurkan saat load scene baru
            DontDestroyOnLoad(gameObject);

            // Tambahkan AudioSource jika belum ada
            backgroundMusicSource = GetComponent<AudioSource>();
            if (backgroundMusicSource == null)
            {
                backgroundMusicSource = gameObject.AddComponent<AudioSource>();
            }

            // Set clip dan loop
            backgroundMusicSource.clip = backgroundMusicClip;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.volume = volume;
            backgroundMusicSource.Play();
        }
        else
        {
            // Jika sudah ada instance, hancurkan GameObject ini agar hanya ada satu
            Destroy(gameObject);
        }
    }

    // Metode opsional untuk mengganti musik jika diperlukan
    public void PlayNewBackgroundMusic(AudioClip newClip)
    {
        if (backgroundMusicSource != null && newClip != null && backgroundMusicSource.clip != newClip)
        {
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = newClip;
            backgroundMusicSource.Play();
        }
    }

    // Metode opsional untuk mengatur volume dari script lain
    public void SetVolume(float newVolume)
    {
        if (backgroundMusicSource != null)
        {
            backgroundMusicSource.volume = Mathf.Clamp01(newVolume);
            volume = backgroundMusicSource.volume;
        }
    }
}