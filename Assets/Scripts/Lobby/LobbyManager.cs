using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Image _blind;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "kr";
        PhotonNetwork.PhotonServerSettings.AppSettings.UseNameServer = true;
        PhotonNetwork.ConnectUsingSettings();

        BGMSoundManager.Instance.PlayClip(eBgmClip.Lobby);
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결 완료");
        _blind.gameObject.SetActive(false);
    }
    public void NewGame()
    {
        PlayerPrefs.DeleteKey("positionX");
        PlayerPrefs.DeleteKey("positionY");
        EffectSoundManager.Instance.PlayClickSound();
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public void ContinueGame()
    { 
        EffectSoundManager.Instance.PlayClickSound();
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Entered Room");
        SceneChangeManager.Instance.LoadInGameScene();
    }
}
