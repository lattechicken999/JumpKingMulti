using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerAutoSave : MonoBehaviourPun
{
    WaitForSeconds _sleep;
    private Coroutine _autoSaveCoroutine;
    private void Awake()
    {
        _sleep = new WaitForSeconds(1);
    }

    private void Start()
    {
        _autoSaveCoroutine = StartCoroutine(AutoSaveCoroutine());
    }
    private void OnDestroy()
    {
        if(_autoSaveCoroutine != null)
            StopCoroutine(_autoSaveCoroutine);
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
}
