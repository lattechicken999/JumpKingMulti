using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoNextScene : MonoBehaviour
{
    [SerializeField] float _loadingDelayTime;
    [SerializeField] Image _fadeoutImg;

    private WaitForSeconds _coroutineDelay;
    private void Start()
    {
        _coroutineDelay = new WaitForSeconds(_loadingDelayTime);
    }
    private void CheckInput()
    {
        if(Keyboard.current.anyKey.isPressed)
        {
            StartCoroutine(FadeoutView());
        }
    }

    IEnumerator FadeoutView()
    {
        Color imgColor = _fadeoutImg.color;
        while (true)
        {
            imgColor.a += 0.01f;
            _fadeoutImg.color = imgColor;
            if (imgColor.a > 0.999)
            {
                SceneChangeManager.Instance.LoadTitleNextScene();
                yield break;
            }
            yield return _coroutineDelay;
        }
    }
    private void Update()
    {
        CheckInput();
    }
}
