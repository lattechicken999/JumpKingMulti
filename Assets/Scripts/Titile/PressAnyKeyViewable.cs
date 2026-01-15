using UnityEngine;
using TMPro;
using System.Collections;

public class PressAnyKeyViewable : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _pressAnyKeyText;
    [SerializeField] float _TwinklingdDlayTime = 0.1f;

    WaitForSeconds _waitDlay;
    bool _increaseDecreaseFlag;

    private void Start()
    {
        _increaseDecreaseFlag = false;
        _waitDlay = new WaitForSeconds(_TwinklingdDlayTime);

        StartCoroutine(TwinklingText());
    }

    private void OnDisable()
    {
        StopCoroutine(TwinklingText());
    }
    IEnumerator TwinklingText()
    {

        Color outlineColor = _pressAnyKeyText.color;
        while (true)
        {
            if (_increaseDecreaseFlag)
            {
                outlineColor.a += 0.1f;
                if (outlineColor.a > 1)
                {
                    outlineColor.a = 1f;
                    _increaseDecreaseFlag = false;
                }
            }
            else
            {
                outlineColor.a -= 0.1f;
                if (outlineColor.a < 0.1)
                {
                    outlineColor.a = 0.1f;
                    _increaseDecreaseFlag = true;
                }
            }
            _pressAnyKeyText.color = outlineColor;
            yield return _waitDlay;
        }
        
    }
}
