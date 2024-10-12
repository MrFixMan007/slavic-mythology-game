using UnityEngine;
using UnityEngine.UI;

public class MusicButtonController : MonoBehaviour
{
    public MusicController musicController;
    public Button calmMusicButton;
    public Button dynamicMusicButton;
    public Button stopMusicButton;

    void Start()
    {
        calmMusicButton.onClick.AddListener(SwitchToCalmMusic);
        dynamicMusicButton.onClick.AddListener(SwitchToDynamicMusic);
        stopMusicButton.onClick.AddListener(StopMusic);
    }

    void SwitchToCalmMusic()
    {
        musicController.SwitchToCalmMusic();
    }

    void SwitchToDynamicMusic()
    {
        musicController.SwitchToDynamicMusic();
    }

    void StopMusic()
    {
        musicController.StopMusic();
    }
}