using System.Collections;
using TMPro;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] tracks;

    private int currentTrack = 0;
    private bool isPaused = false;
    private bool isMuted = false;
    private bool isFading = false;

    private float userVolume = 0.2f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 👇 Permette di suonare anche quando il gioco è in pausa
        audioSource.ignoreListenerPause = true;

        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        audioSource.volume = savedVolume;
        userVolume = savedVolume;
        audioSource.loop = false;
        audioSource.playOnAwake = false;
    }

    void Start()
    {
        if (tracks == null || tracks.Length == 0)
        {
            Debug.LogWarning("MusicPlayer: nessuna traccia trovata!");
            return;
        }

        audioSource.clip = tracks[currentTrack];
        audioSource.Play();

        StartCoroutine(FadeIn(audioSource, userVolume, 1f));
    }

    void Update()
    {
        if (audioSource == null)
            return;

        // Quando finisce naturalmente → passa al prossimo brano
        if (!audioSource.isPlaying && !isPaused && !isMuted && audioSource.clip != null && !isFading)
        {
            NextTrack();
        }
    }

    public void TogglePlayPause()
    {
        if (audioSource == null)
            return;

        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isPaused = true;
        }
        else
        {
            audioSource.Play();
            isPaused = false;
        }
    }

    public bool IsPlaying()
    {
        return audioSource != null && audioSource.isPlaying;
    }

    public void NextTrack()
    {
        if (tracks == null || tracks.Length == 0 || audioSource == null)
            return;

        StopAllCoroutines();
        isFading = false;

        currentTrack = (currentTrack + 1) % tracks.Length;
        Debug.Log($"▶ NextTrack(): {tracks[currentTrack].name}");
        StartCoroutine(FadeOutIn(tracks[currentTrack], 0.6f));
    }

    public void PreviousTrack()
    {
        if (tracks == null || tracks.Length == 0 || audioSource == null)
            return;

        StopAllCoroutines();
        isFading = false;

        currentTrack = (currentTrack - 1 + tracks.Length) % tracks.Length;
        Debug.Log($"⏮ PreviousTrack(): {tracks[currentTrack].name}");
        StartCoroutine(FadeOutIn(tracks[currentTrack], 0.6f));
    }

    public void ToggleMute()
    {
        if (audioSource == null)
            return;

        isMuted = !isMuted;
        audioSource.mute = isMuted;
    }

    public void ChangeVolume(float value)
    {
        if (audioSource == null)
            return;

        userVolume = Mathf.Clamp01(value);
        audioSource.volume = userVolume;

        PlayerPrefs.SetFloat("MusicVolume", userVolume);
        PlayerPrefs.Save();
    }

    public string GetCurrentTrackName()
    {
        return audioSource != null && audioSource.clip != null ? audioSource.clip.name : "(none)";
    }

    IEnumerator FadeOutIn(AudioClip newClip, float duration)
    {
        if (audioSource == null || newClip == null)
            yield break;

        isFading = true;
        float startVol = audioSource.volume;

        // Fade-out indipendente dal TimeScale
        float t = 0f;
        while (t < duration * 0.5f)
        {
            if (audioSource == null)
                yield break;
            t += Time.unscaledDeltaTime; // indipendente dal Time.timeScale
            audioSource.volume = Mathf.Lerp(startVol, 0f, t / (duration * 0.5f));
            yield return null;
        }

        // Cambio clip e riavvio anche in pausa
        audioSource.clip = newClip;
        audioSource.time = 0f;
        audioSource.Play();

        // Fade-in
        t = 0f;
        while (t < duration * 0.5f)
        {
            if (audioSource == null)
                yield break;
            t += Time.unscaledDeltaTime; 
            audioSource.volume = Mathf.Lerp(0f, userVolume, t / (duration * 0.5f));
            yield return null;
        }

        audioSource.volume = userVolume;
        isFading = false;
    }

    IEnumerator FadeIn(AudioSource source, float targetVolume, float duration)
    {
        if (source == null)
            yield break;

        isFading = true;
        source.volume = 0f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime; // fade indipendente dal TimeScale
            source.volume = Mathf.Lerp(0f, userVolume, t / duration);
            yield return null;
        }

        source.volume = userVolume;
        isFading = false;
    }

    public void UpdateTrackNameUI(TMP_Text label)
    {
        if (label == null)
            return;

        label.text =
            audioSource != null && audioSource.clip != null
                ? $"Now Playing: {audioSource.clip.name}"
                : "Now Playing: (none)";
    }
}
