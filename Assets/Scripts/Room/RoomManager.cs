using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _playerPrefeb;
    [SerializeField] Transform _startPotint;
    [SerializeField] float _randomPoint;

    private void Start()
    {
        JoinRoom();
    }
    public void JoinRoom()
    {
        Vector3 spawnPoint = _startPotint.position;
        spawnPoint.x = spawnPoint.x + Random.Range(-_randomPoint, _randomPoint);
        PhotonNetwork.Instantiate(_playerPrefeb.name, spawnPoint,Quaternion.identity);
    }
    public void ExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public override void OnLeftRoom()
    {
        Debug.Log("방을 나갔습니다.");
        GameManager.Instance.LoadLobbyScene();
    }
}
