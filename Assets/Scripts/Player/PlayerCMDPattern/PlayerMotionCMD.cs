using System.Collections;
using UnityEngine;

public interface IPlayerCommand
{
    void Execute();
    IEnumerator Replay();
}

public class MoveCommand : IPlayerCommand
{
    Vector2 _dir;
    PlayerInvoker _player;
    public MoveCommand(PlayerInvoker _player, Vector2 dir)
    {
        this._dir = dir;
        this._player = _player;
    }
    public void Execute()
    {
        _player.SetMoveDir(_dir);
    }

    public IEnumerator Replay()
    {
        Execute();
        yield return null;
    }
}
public class JumpCommand : IPlayerCommand
{
    bool _isJump;
    PlayerInvoker _player;

    public JumpCommand(PlayerInvoker player, bool isJump)
    {
        this._player = player;
        this._isJump = isJump;
    }
    public void Execute()
    {
        if(_isJump)
        {
            _player.JumpStart();
        }
        else
        {
            _player.JumpEnd();
        }
    }

    public IEnumerator Replay()
    {
        Execute();
        yield return null;
    }
}
public class WaitCommand : IPlayerCommand
{
    float _waitTime;
    WaitForSeconds _waitRetern;
    public WaitCommand(float time)
    {
        _waitTime = time;
        _waitRetern = new WaitForSeconds(_waitTime);
    }
    public void Execute()
    {

    }
    public IEnumerator Replay()
    {
        yield return _waitRetern;
    }
}

