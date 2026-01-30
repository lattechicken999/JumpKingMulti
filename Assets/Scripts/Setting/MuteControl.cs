using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MuteControl : MonoBehaviour
{
    [SerializeField] Toggle _muteToggle;
    private bool muted;

    //private void Awake()
    //{
    //    if(PlayerPrefs.HasKey("Mute"))
    //    {
    //        BGMSoundManager.Instance.SetBGMMuteState(PlayerPrefs.GetInt("Mute") == 1);
    //    }
    //}
    private void OnEnable()
    {
        muted = BGMSoundManager.Instance.GetBGMMuteState();
        _muteToggle.isOn = muted;
    }
    public void Mute()
    {
        muted = !muted;
        PlayerPrefs.SetInt("Mute",muted ? 1 : 0);
        BGMSoundManager.Instance.SetBGMMuteState(muted);
    }
}
