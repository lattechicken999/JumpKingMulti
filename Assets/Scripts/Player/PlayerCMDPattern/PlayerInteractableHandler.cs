using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractableHandler : MonoBehaviourPun
{
    //[SerializeField] eCollisionDir _colDir;
    [SerializeField] PlayerInputHandler _playerHandler;

    private Rigidbody2D _rig;
    private void Awake()
    {
        _rig = transform.GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!photonView.IsMine) return;
        foreach(var contact in collision.contacts)
        {
            var colsVector = contact.normal;
            Vector2 convertVelocity = collision.relativeVelocity;

            //법선 백터 방식으로 변경
            if (colsVector.y > 0.9f)
              {
                _playerHandler.RecordAndExcute(new OnGroundCommand(_playerHandler.Player, true));
                return;
            }
            convertVelocity *= 0.8f;
            convertVelocity *= new Vector2(1,-1);

            _playerHandler.RecordAndExcute(new CollisionCommmand(_playerHandler.Player, convertVelocity));
        }
       
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.contacts.Length == 0)
        {
            _playerHandler.RecordAndExcute(new OnGroundCommand(_playerHandler.Player, false));
            return;
        }
        if (collision.contacts[0].normal.y < -0.9f)
        {
            _playerHandler.RecordAndExcute(new OnGroundCommand(_playerHandler.Player, false));
            return;
        }
    }
}
