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
        Vector3 spawnPoint = _startPotint.position;
        spawnPoint.x = spawnPoint.x + Random.Range(-_randomPoint, _randomPoint);
        var  playerObject = PhotonNetwork.Instantiate(_playerPrefeb.name, spawnPoint,Quaternion.identity);
        var NickNameControl = playerObject.GetComponentInChildren<PlayerNickNameControl>();

        NickNameControl.SetNickName( FirebaseDbManager.Instance.GetUserNickName());

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
