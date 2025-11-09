using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicUIImmortal : MonoBehaviour
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
        ConnectUI();
    }

    void ConnectUI()
    {
        if (MusicPlayer.Instance == null)
        {
            Debug.LogWarning("MusicPlayer non trovato nella scena!");
            return;
        }

        playPauseImage = playPauseButton.GetComponent<Image>();

        // Volume
        volumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        volumeSlider.onValueChanged.AddListener(MusicPlayer.Instance.ChangeVolume);

        // Bottoni
        nextButton.onClick.AddListener(() => MusicPlayer.Instance.NextTrack());
        prevButton.onClick.AddListener(() => MusicPlayer.Instance.PreviousTrack());
        muteButton.onClick.AddListener(() => MusicPlayer.Instance.ToggleMute());
        playPauseButton.onClick.AddListener(OnPlayPause);

        UpdateUI();
    }

    void Update()
    {
        if (MusicPlayer.Instance == null)
            return;
        UpdateUI();
    }

    void OnPlayPause()
    {
        if (MusicPlayer.Instance == null)
            return;
        MusicPlayer.Instance.TogglePlayPause();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (MusicPlayer.Instance == null)
            return;

        if (trackNameText != null)
            trackNameText.text = $"Now Playing: {MusicPlayer.Instance.GetCurrentTrackName()}";

        if (playPauseImage != null && playIcon != null && pauseIcon != null)
            playPauseImage.sprite = MusicPlayer.Instance.IsPlaying() ? pauseIcon : playIcon;
    }
}
