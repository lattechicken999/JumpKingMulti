using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] PlayerInputHandler _playerHandler;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _playerHandler.RecordAndExcute(new OnGroundCommand(_playerHandler.Player, true));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _playerHandler.RecordAndExcute(new OnGroundCommand(_playerHandler.Player, false));
    }
}
