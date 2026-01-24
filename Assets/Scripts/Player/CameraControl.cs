using Photon.Pun;
using UnityEngine;

public class CameraControl : MonoBehaviourPun
{
    float _camMovePoint;
    private Renderer _renderer;

    bool _viewPortUpdateFlag = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _camMovePoint = Camera.main.orthographicSize*2;
    }

    private void Update()
    {
        if (!photonView.IsMine) return;
        if(!_renderer.isVisible)
        {
            if(!_viewPortUpdateFlag)
            {
                _viewPortUpdateFlag = true;
                Vector2 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
                if(viewPoint.y > 1)
                {
                    Camera.main.transform.position +=new Vector3(0, _camMovePoint,0);
                }
                else if(viewPoint.y < 0)
                {
                    Camera.main.transform.position -= new Vector3(0, _camMovePoint, 0);
                }
            }
            else
            {
                Debug.LogError("카메라 위치 갱신 로직이 이상함.");
            }
        }
        else
        {
            _viewPortUpdateFlag = false;
        }
    }
}
