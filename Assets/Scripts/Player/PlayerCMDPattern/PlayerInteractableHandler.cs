using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractableHandler : MonoBehaviourPun
{
    [SerializeField] eCollisionDir _colDir;
    [SerializeField] PlayerInputHandler _playerHandler;
    private void Awake()
    {
        if(!photonView.IsMine)
        {
            transform.GetComponent<Collider2D>().enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerHandler.RecordAndExcute(new CollisionCommmand(_playerHandler.Player,_colDir));
    }
}
