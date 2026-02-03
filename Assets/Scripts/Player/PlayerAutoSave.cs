using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerAutoSave : MonoBehaviourPun,IGameClearOpserver
{
    WaitForSeconds _sleep;
    private Coroutine _autoSaveCoroutine;
    private void Awake()
    {
        _sleep = new WaitForSeconds(1);
    }

    private void Start()
    {
        if (!photonView.IsMine) return;
        _autoSaveCoroutine = StartCoroutine(AutoSaveCoroutine());
        InGameManager.Instance.RegistGameClearSub(this);
    }
    private void OnDestroy()
    {
        if (!photonView.IsMine) return;
        if (_autoSaveCoroutine != null)
            StopCoroutine(_autoSaveCoroutine);
        InGameManager.Instance.UnregistGameClearSub(this);  
    }
    private IEnumerator AutoSaveCoroutine()
    {
        if (!photonView.IsMine) yield break;
        while(true)
        {
            PlayerPrefs.SetFloat("positionX",transform.position.x);
            PlayerPrefs.SetFloat("positionY",transform.position.y);
            PlayerPrefs.Save();
            yield return _sleep;

        }
        
    }

    public void GameClear()
    {
        if (_autoSaveCoroutine != null)
            StopCoroutine(_autoSaveCoroutine);
        PlayerPrefs.DeleteKey("positionX");
        PlayerPrefs.DeleteKey("positionY");
    }
}
