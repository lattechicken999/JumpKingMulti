using System.Collections;
using UnityEngine;

public class PlayerAutoSave : MonoBehaviour
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
        StopCoroutine(_autoSaveCoroutine);
    }
    private IEnumerator AutoSaveCoroutine()
    {
        while(true)
        {
            PlayerPrefs.SetFloat("positionX",transform.position.x);
            PlayerPrefs.SetFloat("positionY",transform.position.y);
            PlayerPrefs.Save();
            yield return _sleep;

        }
        
    }
}
