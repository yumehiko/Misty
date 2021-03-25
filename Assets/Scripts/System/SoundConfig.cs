using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UniRx;

public class SoundConfig : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer = default;
    [SerializeField] private Slider masterSlider = default;
    [SerializeField] private Slider musicSlider = default;
    [SerializeField] private Slider soundSlider = default;

    private readonly string aKeyMasterVolume = "MasterVolume";
    private readonly string aKeyMusicVolume = "MusicVolume";
    private readonly string aKeySoundVolume = "SoundVolume";

    private void Start()
    {
        LoadPrefs();
        _ = PauseMenu.PauseMenuInstance.OnPause
            .Where(isActive => isActive == false)
            .Subscribe(isActive => SaveToPrefs());
    }

    /// <summary>
    /// PlayerPrefのデータをロード。
    /// </summary>
    private void LoadPrefs()
    {
        masterSlider.value = PlayerPrefs.GetFloat(aKeyMasterVolume, 0.8f);
        musicSlider.value = PlayerPrefs.GetFloat(aKeyMusicVolume, 0.5f);
        soundSlider.value = PlayerPrefs.GetFloat(aKeySoundVolume, 0.6f);
        OnSliderMaster();
        OnSliderMusic();
        OnSliderSound();
    }

    /// <summary>
    /// 現在値をPlayerPrefに保存。
    /// </summary>
    private void SaveToPrefs()
    {
        PlayerPrefs.SetFloat(aKeyMasterVolume, masterSlider.value);
        PlayerPrefs.SetFloat(aKeyMusicVolume, musicSlider.value);
        PlayerPrefs.SetFloat(aKeySoundVolume, soundSlider.value);
    }

    /// <summary>
    /// マスター音量スライダーから呼び出し。
    /// AudioMixerの設定を変える。
    /// </summary>
    public void OnSliderMaster()
    {
        float value = ValueToDecibel(masterSlider.value);
        audioMixer.SetFloat(aKeyMasterVolume, value);
    }

    /// <summary>
    /// ミュージック音量スライダーから呼び出し。
    /// AudioMixerの設定を変える。
    /// </summary>
    public void OnSliderMusic()
    {
        float value = ValueToDecibel(musicSlider.value);
        audioMixer.SetFloat(aKeyMusicVolume, value);
    }

    /// <summary>
    /// サウンド音量スライダーから呼び出し。
    /// AudioMixerの設定を変える。
    /// </summary>
    public void OnSliderSound()
    {
        float value = ValueToDecibel(soundSlider.value);
        audioMixer.SetFloat(aKeySoundVolume, value);
    }

    /// <summary>
    /// 正規値をデシベルに変換。
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private float ValueToDecibel(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1.0f);
        return 20.0f * Mathf.Log10(value);
    }
}