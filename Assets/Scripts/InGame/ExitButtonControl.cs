using Photon.Pun;
using UnityEngine;

public class ExitButtonControl : MonoBehaviourPunCallbacks
{
    public void OnExitIngame()
    {
        InGameManager.Instance.UnregistAll();

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Room 나가기 완료");
        SceneChangeManager.Instance.LoadLobbyScene();
    }
}
