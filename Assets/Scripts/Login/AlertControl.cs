using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class AlertControl : MonoBehaviour
{
    [Header("parameter")]
    [SerializeField] float _viewDelay = 0.1f;
    [SerializeField] float _dAlpha = 0.1f;
    [SerializeField] float _alertDisplayTime = 1.5f;
    [SerializeField] float _blank = 20f;

    [Header("Connect Component")]
    [SerializeField] Image _panelImg;
    [SerializeField] TextMeshProUGUI _messageText;
    [SerializeField] RectTransform _PannelTransform;
    [SerializeField] RectTransform _TextTransform;


    private Color _panelColor, _TextColor;
    private WaitForSeconds _coroutineDelay;

    private Coroutine _alertStartCoroutine, _alertCloseCoroutine;
    private void Awake()
    {
        _coroutineDelay = new WaitForSeconds(_viewDelay);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        UpdateAlpha(0);
    }
    private void OnDestroy()
    {
        if(_alertStartCoroutine != null)
            StopCoroutine(_alertStartCoroutine);
        if(_alertCloseCoroutine != null)
            StopCoroutine(_alertCloseCoroutine);
    }
    private void UpdateAlpha(float alpha)
    {
        _panelColor = _panelImg.color;
        _TextColor = _messageText.color;

        _panelColor.a = alpha;
        _TextColor.a = alpha;

        _panelImg.color = _panelColor;
        _messageText.color = _TextColor;
    }
    private void AlertStart()
    {
        gameObject.SetActive(true);
        _alertStartCoroutine = StartCoroutine(AlertStartCoroutine());
    }
    private IEnumerator AlertStartCoroutine()
    {
        float alpha = 0f;
        while (true)
        {
            alpha += _dAlpha;
            UpdateAlpha(alpha);
            if(alpha >= 1)
            {
                yield return new WaitForSeconds(_alertDisplayTime);
                _alertCloseCoroutine = StartCoroutine(AlertCloseCoroutine());
                yield break;
            }
            yield return _coroutineDelay;
        }
    }

    private IEnumerator AlertCloseCoroutine()
    {
        float alpha = 1f;
        while (true)
        {
            alpha -= _dAlpha;
            UpdateAlpha(alpha);
            if (alpha <= 0)
            {
                Destroy(gameObject);
                yield break;
            }
            yield return _coroutineDelay;
        }
    }


    public void UpdateTextMessage(string message)
    {
        _messageText.text = message;
        var rectSize = _PannelTransform.sizeDelta;
        rectSize.x = _messageText.preferredWidth + _blank;
        _PannelTransform.sizeDelta = rectSize;

        rectSize = _TextTransform.sizeDelta;
        rectSize.x = _messageText.preferredWidth;
        _TextTransform.sizeDelta = rectSize;

        AlertStart();
    }

}
