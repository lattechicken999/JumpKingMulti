using Photon.Pun;
using UnityEngine;
using Unity.Cinemachine;

public class CameraControl : MonoBehaviourPun,IGameClearOpserver
{
    private CinemachineCamera _cam;
    private CinemachineCamera _clearCam;
    float _camMovePoint;

    public void GameClear()
    {
        _cam.Priority = 0;
        _clearCam.Priority = 1;
    }

    private void Awake()
    {
        if (photonView.IsMine)
        {
            _cam = GameObject.Find("CineCam").GetComponent<CinemachineCamera>();
            _clearCam = GameObject.Find("CineClearCam").GetComponent<CinemachineCamera>();
            _camMovePoint = _cam.Lens.OrthographicSize * 2;
            _cam.Priority = 1;
            _clearCam.Priority = 0;
            _clearCam.Target.TrackingTarget = transform;
           
        }

    }
    private void Start()
    {
        if (photonView.IsMine)
            InGameManager.Instance.RegistGameClearSub(this);
    }
    private void OnDestroy()
    {
        if (photonView.IsMine)
            InGameManager.Instance.UnregistGameClearSub(this);
    }
    private void OnBecameInvisible()
    {
        if (!photonView.IsMine) return;
        Vector2 viewPoint = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPoint.y > 1)
        {
            _cam.transform.position += new Vector3(0, _camMovePoint, 0);
        }
        else if (viewPoint.y < 0)
        {
            _cam.transform.position -= new Vector3(0, _camMovePoint, 0);
        }
    }
}
