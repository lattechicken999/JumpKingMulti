using UnityEngine;
using UnityEngine.UI;

public class SoundVolumeControl : MonoBehaviour
{
    [SerializeField] Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider.value = BGMSoundManager.Instance.GetBGMSoundVolume();
    }
    public void ChangeVolume()
    {
        BGMSoundManager.Instance.BGMSoundVolumChange(_volumeSlider.value);
    }
}
