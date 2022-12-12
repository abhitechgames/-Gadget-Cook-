using UnityEngine;
using MoreMountains.NiceVibrations;

public class SplashEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem splashEffect;
    private void OnCollisionEnter(Collision other) // Just Add a Splash Effect when Dipped in Maida
    {
        splashEffect.Play();
        AudioManager.instance.Play("MaidaDrop");

        MMVibrationManager.Haptic (HapticTypes.LightImpact);
    }
}
