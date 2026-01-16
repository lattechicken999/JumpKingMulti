using UnityEngine;
using DG.Tweening;
public class PanelOpenCloseAnimation : MonoBehaviour
{

    private void Start()
    {
        DOTween.Init();
        transform.localScale = Vector3.one * 0.03f;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);

        var seq = DOTween.Sequence();

        seq.Append(transform.DOScale(1.1f, 0.2f));
        seq.Append(transform.DOScale(1f, 0.1f));

        seq.Play();
    }

    public void HIde()
    {
        var seq = DOTween.Sequence();
        transform.localScale = Vector3.one * 0.2f;

        seq.Append(transform.DOScale(1.1f, 0.1f));
        seq.Append(transform.DOScale(0.01f, 0.1f));

        seq.Play().OnComplete(()=> gameObject.SetActive(false));
    }
}
