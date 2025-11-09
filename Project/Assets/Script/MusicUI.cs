using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider volumeSlider;
    public Button nextButton;
    public Button prevButton;
    public Button playPauseButton;
    public Button muteButton;
    public TextMeshProUGUI trackNameText;

    [Header("Play/Pause Sprites")]
    public Sprite playIcon;
    public Sprite pauseIcon;

    private Image playPauseImage;

    void Start()
    {
        if (MusicPlayer.Instance == null)
        {
            Debug.LogWarning("MusicPlayer non trovato!");
            return;
        }

        // Imposta il volume iniziale dallo stato salvato
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        volumeSlider.onValueChanged.AddListener(MusicPlayer.Instance.ChangeVolume);

        // Collega i bottoni
        nextButton.onClick.AddListener(OnNext);
        prevButton.onClick.AddListener(OnPrev);
        muteButton.onClick.AddListener(MusicPlayer.Instance.ToggleMute);
        playPauseButton.onClick.AddListener(OnPlayPause);

        // Icona pulsante Play/Pausa
        playPauseImage = playPauseButton.GetComponent<Image>();
        UpdatePlayPauseIcon();

        UpdateTrackName();
    }

    void Update()
    {
        // Aggiorna il nome del brano e l’icona in tempo reale
        if (trackNameText != null && MusicPlayer.Instance != null)
            trackNameText.text = $"Now Playing: {MusicPlayer.Instance.GetCurrentTrackName()}";

        UpdatePlayPauseIcon();
    }

    void OnNext()
    {
        MusicPlayer.Instance.NextTrack();
        UpdateTrackName();
    }

    void OnPrev()
    {
        MusicPlayer.Instance.PreviousTrack();
        UpdateTrackName();
    }

    void OnPlayPause()
    {
        MusicPlayer.Instance.TogglePlayPause();
        UpdatePlayPauseIcon();
    }

    void UpdateTrackName()
    {
        if (trackNameText == null)
            return;
        trackNameText.text = $"Now Playing: {MusicPlayer.Instance.GetCurrentTrackName()}";
    }

    void UpdatePlayPauseIcon()
    {
        if (playPauseImage == null || playIcon == null || pauseIcon == null)
            return;

        // Se la musica è in riproduzione → mostra “Pause”, altrimenti “Play”
        playPauseImage.sprite = MusicPlayer.Instance.IsPlaying() ? pauseIcon : playIcon;
    }
}
