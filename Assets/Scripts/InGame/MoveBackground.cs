using System.Collections;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] Transform _backgroundImg;
    private float _moveStack;
    private Vector3 _originPosition;
    private Coroutine _moveCoroutine;
    private void Awake()
    {
        _originPosition = _backgroundImg.position;
        _moveStack = 0;
    }

    private void Start()
    {
        _moveCoroutine = StartCoroutine(moveCoroutine());
    }

    private void OnDestroy()
    {
        if(_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
        }
    }
    IEnumerator moveCoroutine()
    {
        Vector3 movePoint = Vector3.left / 300;
        while (true)
        {
            _backgroundImg.transform.position += movePoint;
            _moveStack += movePoint.x;
            if (_moveStack < -30)
            {
                _moveStack = 0;
                _backgroundImg.transform.position = _originPosition;
            }
            yield return null;
        }

    }
}
