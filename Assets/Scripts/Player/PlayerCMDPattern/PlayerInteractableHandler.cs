using UnityEngine;

public class PlayerInteractableHandler : MonoBehaviour
{
    [SerializeField] eCollisionDir _colDir;
    [SerializeField] PlayerInputHandler _playerHandler;
    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerHandler.RecordAndExcute(new CollisionCommmand(_playerHandler.Player,_colDir));
    }
}
