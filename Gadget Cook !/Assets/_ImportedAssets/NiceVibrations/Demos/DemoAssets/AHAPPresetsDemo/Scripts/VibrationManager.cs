using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;

[System.Serializable]
public class PresetItem
{
    public string Name;
    public TextAsset AHAPFile;
    public MMNVAndroidWaveFormAsset WaveFormAsset;
    public MMNVRumbleWaveFormAsset RumbleWaveFormAsset;
}

public class VibrationManager : MonoBehaviour
{
    public List<PresetItem> Items;

    public static VibrationManager instance;

    VibrationManager()
    {
        instance = this;
    }

    private void Start()
    {
        VibrationSetup();
    }

    public void VibrationSetup()
    {
        if (PlayerPrefs.GetInt("vibrationsOption", 0) == 0)
        {
            // turns all haptics on
            MMVibrationManager.SetHapticsActive(true);
        }
        else
        {
            // turns all haptics off
            MMVibrationManager.SetHapticsActive(false);
        }
    }

    public void LaserHaptics()
    {
        int index = 0;

        MMVibrationManager.AdvancedHapticPattern(Items[index].AHAPFile.text, Items[index].WaveFormAsset.WaveForm.Pattern, Items[index].WaveFormAsset.WaveForm.Amplitudes, -1, Items[index].RumbleWaveFormAsset.WaveForm.Pattern, Items[index].RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes, Items[index].RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1, HapticTypes.LightImpact, this, -1, true);
    }

    public void ReloadHaptics()
    {
        int index = 1;

        MMVibrationManager.AdvancedHapticPattern(Items[index].AHAPFile.text, Items[index].WaveFormAsset.WaveForm.Pattern, Items[index].WaveFormAsset.WaveForm.Amplitudes, -1, Items[index].RumbleWaveFormAsset.WaveForm.Pattern, Items[index].RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes, Items[index].RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1, HapticTypes.LightImpact, this, -1, true);
    }

    public void PortalHaptics()
    {
        int index = 2;

        MMVibrationManager.AdvancedHapticPattern(Items[index].AHAPFile.text, Items[index].WaveFormAsset.WaveForm.Pattern, Items[index].WaveFormAsset.WaveForm.Amplitudes, -1, Items[index].RumbleWaveFormAsset.WaveForm.Pattern, Items[index].RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes, Items[index].RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1, HapticTypes.LightImpact, this, -1, true);
    }

    public void GameOverHaptics()
    {
        int index = 3;

        MMVibrationManager.AdvancedHapticPattern(Items[index].AHAPFile.text, Items[index].WaveFormAsset.WaveForm.Pattern, Items[index].WaveFormAsset.WaveForm.Amplitudes, -1, Items[index].RumbleWaveFormAsset.WaveForm.Pattern, Items[index].RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes, Items[index].RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1, HapticTypes.LightImpact, this, -1, true);
    }
}

















//  // for the purpose of the demo, and to be able to observe the difference, if any, on certain devices,
//  // the first 4 effects (dice, drums, game over, heart beats) will be called on the main thread, and the remaining ones on a secondary thread
//     if (index < 5)
//     {
//         MMVibrationManager.AdvancedHapticPattern(DemoItems[index].AHAPFile.text,
//                                                 DemoItems[index].WaveFormAsset.WaveForm.Pattern, DemoItems[index].WaveFormAsset.WaveForm.Amplitudes, -1,
//                                                 DemoItems[index].RumbleWaveFormAsset.WaveForm.Pattern, DemoItems[index].RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes,
//                                                 DemoItems[index].RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1,
//                                                 HapticTypes.LightImpact, this, -1, false); 
//     }
//     else
//     {
//     }
