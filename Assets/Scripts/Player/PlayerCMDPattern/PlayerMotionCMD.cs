using System.Collections;
using UnityEngine;

public interface IPlayerCommand
{
    void Execute();
    IEnumerator Replay();
}
public enum eCollisionDir
{ 
    Up, Down, Left, Right,_End
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

public class OnGroundCommand : IPlayerCommand
{
    PlayerInvoker _player;
    bool _isGround;
    public OnGroundCommand(PlayerInvoker player,bool isGround   )
    {
        _player = player;
        _isGround = isGround;
    }
    public void Execute()
    {
        _player.OnGround(_isGround);
    }

    public IEnumerator Replay()
    {
        Execute();
        yield return null;
    }
}

public class CollisionCommmand : IPlayerCommand
{
    PlayerInvoker _player;
    Vector2 _playerActiveVelocity;
    public CollisionCommmand(PlayerInvoker player, Vector2 velocity)
    {
        this._player = player;
        this._playerActiveVelocity = velocity;
    }

    public void Execute()
    {
        //_player.PlayerCollisionAct(_collisionDirection);
        _player.PlayerCollisionAct(_playerActiveVelocity);
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

