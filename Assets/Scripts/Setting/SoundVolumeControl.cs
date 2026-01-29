using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeControl : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;

    public void ChangeVolume()
    {
        BGMSoundManager.Instance.BGMSoundVolumChange(_volumeSlider.value);
    }
}
