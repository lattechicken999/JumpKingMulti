using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MuteControl : MonoBehaviour
{
    [SerializeField] Toggle _muteToggle;
    private bool muted;
    private bool _isInitFlag = false;

    private void OnEnable()
    {
        muted = BGMSoundManager.Instance.GetBGMMuteState();
        _isInitFlag = _muteToggle.isOn != muted;
        _muteToggle.isOn = muted;
         
    }
    public void Mute()
    {
        if(_isInitFlag)
        {
            //초기화로 인한 OnValueChanged 는 무시함
            _isInitFlag = false;
            return;
        }
        muted = !muted;
        PlayerPrefs.SetInt("Mute",muted ? 1 : 0);
        BGMSoundManager.Instance.SetBGMMuteState(muted);
    }
}
