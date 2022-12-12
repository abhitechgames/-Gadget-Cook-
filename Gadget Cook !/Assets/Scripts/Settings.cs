using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using TMPro;
public class Settings : MonoBehaviour
{
    #region Variables
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private TMP_Text soundText;
    [SerializeField] private TMP_Text hapticsText;
    [SerializeField] private TMP_Text postfxText;
    #endregion

    private void Start() // Updates Settings Options at Start
    {
        PlayerPrefs.GetInt("Sound", 1);
        PlayerPrefs.GetInt("Haptics", 1);
        PlayerPrefs.GetInt("PostFX", 1);

        if(PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            audioMixer.SetFloat("volume", 0f);
            soundText.text = "ON";
        }
        else
        {
            audioMixer.SetFloat("volume", -80f);
            soundText.text = "OFF";
        }
        
        if(PlayerPrefs.GetInt("Haptics", 1) == 1)
        {
            hapticsText.text = "ON";
            MoreMountains.NiceVibrations.MMVibrationManager.StopAllHaptics(true);
        }
        else
        {
            hapticsText.text = "OFF";
            MoreMountains.NiceVibrations.MMVibrationManager.StopAllHaptics(false);
        }

        if(PlayerPrefs.GetInt("PostFX", 1) == 1)
        {
            FindObjectOfType<PostProcessVolume>().enabled = true;
            postfxText.text = "ON";
        }
        else
        {
            FindObjectOfType<PostProcessVolume>().enabled = false;
            postfxText.text = "OFF";
        }
    }

    public void SoundSettings()
    {
        if(PlayerPrefs.GetInt("Sound", 1) == 1)
        {
            soundText.text = "OFF";
            PlayerPrefs.SetInt("Sound", 0);
            audioMixer.SetFloat("volume", -80f);
        }
        else
        {
            soundText.text = "ON";
            PlayerPrefs.SetInt("Sound", 1);
            audioMixer.SetFloat("volume", 0f);
        }
    }

    public void HapticsSettings()
    {
        if(PlayerPrefs.GetInt("Haptics", 1) == 1)
        {
            hapticsText.text = "OFF";
            PlayerPrefs.SetInt("Haptics", 0);
            MoreMountains.NiceVibrations.MMVibrationManager.StopAllHaptics(false);
        }
        else
        {
            hapticsText.text = "ON";
            PlayerPrefs.SetInt("Haptics", 1);
            MoreMountains.NiceVibrations.MMVibrationManager.StopAllHaptics(true);
        }
    }

    public void PostFXSettings()
    {
        if(PlayerPrefs.GetInt("PostFX", 1) == 1)
        {
            postfxText.text = "OFF";
            PlayerPrefs.SetInt("PostFX", 0);
            FindObjectOfType<PostProcessVolume>().enabled = false;
        }
        else
        {
            postfxText.text = "ON";
            PlayerPrefs.SetInt("PostFX", 1);
            FindObjectOfType<PostProcessVolume>().enabled = true;
        }
    }
}
