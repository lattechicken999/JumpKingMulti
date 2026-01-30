using UnityEngine;
using Photon.Pun;
using System.Collections;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _playerPrefeb;
    [SerializeField] Transform _startPotint;
    [SerializeField] float _randomPoint;


    private IEnumerator Start()
    {
        yield return new WaitUntil(() => PhotonNetwork.InRoom && PhotonNetwork.IsConnectedAndReady);
        JoinRoom();
    }
    public void JoinRoom()
    {
        Vector2 playerPosition;
        if (PlayerPrefs.HasKey("positionX"))
        {
            playerPosition = new Vector2(PlayerPrefs.GetFloat("positionX"),
                                                               PlayerPrefs.GetFloat("positionY"));
        }
        else
        {
            playerPosition = _startPotint.position;
            playerPosition.x = playerPosition.x + Random.Range(-_randomPoint, _randomPoint);
        }

        var  playerObject = PhotonNetwork.Instantiate(_playerPrefeb.name, playerPosition, Quaternion.identity);
        var NickNameControl = playerObject.GetComponentInChildren<PlayerNickNameControl>();

        NickNameControl.SetNickName( FirebaseDbManager.Instance.GetUserNickName());

        BGMSoundManager.Instance.PlayClip(eBgmClip.InGame);
    }
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("방을 나갔습니다.");
        SceneChangeManager.Instance.LoadLobbyScene();
    }

}
