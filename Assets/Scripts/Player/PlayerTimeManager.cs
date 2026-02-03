using UnityEngine;
using Photon.Pun;
using System.Collections;
public class PlayerTimeManager : MonoBehaviourPun,IGameClearOpserver
{

    static float _playTime = 0;
    public static float PlayTime => _playTime;

    bool _isPlaying;


    private void Start()
    {
        if(!photonView.IsMine) { return; }
        if(PlayerPrefs.HasKey("Time"))
        {
            _playTime = PlayerPrefs.GetFloat("Time");
        }
        else
        {
            _playTime = 0;
        }
        _isPlaying = true;
        InGameManager.Instance.RegistGameClearSub(this);
    }
    private void OnDestroy()
    {
        if (!photonView.IsMine) { return; }
        InGameManager.Instance.UnregistGameClearSub(this);
    }
    void Update()
    {
        if (!photonView.IsMine) { return; }
        if (_isPlaying)
        {
            _playTime += Time.deltaTime;
            PlayerPrefs.SetFloat("Time",_playTime);
        }
    }
    public void GameClear()
    {
        _isPlaying = false;
        FirebaseDbManager.Instance.SaveUserClearTime((long)(PlayTime*1000));
        PlayerPrefs.DeleteKey("Time");

    }
}
