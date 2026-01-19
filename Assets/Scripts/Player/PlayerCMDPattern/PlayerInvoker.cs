using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerInvoker : MonoBehaviourPun
{
    [SerializeField] float _increaseGageValue = 1f;
    [SerializeField] float _minimumGageValue = 0.8f;
    [SerializeField] float _maximumGageValue = 10f;
    [SerializeField] float _moveSpeed = 10f;

    private Rigidbody2D _rig;
    private Animator _ani;

    private bool _isPressJumpkey;
    private float _jumpGage;
    private bool _isGround;
    private Vector2 _dir;
    private Vector2 _collisionedVelocity;

    private void Start()
    {
        _ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();

        _dir = Vector2.zero; 
        _isPressJumpkey = false;
        _jumpGage = 0f;
        _collisionedVelocity = Vector2.zero;
    }

    public void SetMoveDir(Vector2 dir)
    {
        _dir = dir;
    }
    public void JumpStart()
    {
        _jumpGage = _minimumGageValue;
        _isPressJumpkey = true;
    }
    public void JumpEnd()
    {
        if (!_isPressJumpkey) return;
        _isPressJumpkey = false;

        float gagePersent = Mathf.Clamp( _jumpGage / _maximumGageValue, _minimumGageValue/_maximumGageValue,0.7f);
        Vector2 jumpDir = _dir * (1 - gagePersent) + Vector2.up * (gagePersent);
        _rig.AddForce(jumpDir.normalized * _jumpGage, ForceMode2D.Impulse);
        //_rig.linearVelocity = _rig.linearVelocity.normalized * 5f;
    }

    public void PlayerCollisionAct(eCollisionDir collisionDirection)
    {
        if (!photonView.IsMine) return;
        switch (collisionDirection)
        {
            case eCollisionDir.Up:
                if (_rig.linearVelocityY > 0)
                    _collisionedVelocity = new Vector2(_rig.linearVelocityX, -_rig.linearVelocityY);
                break;
            case eCollisionDir.Left:
                if (_rig.linearVelocityX < 0)
                    _collisionedVelocity = new Vector2(-_rig.linearVelocityX, _rig.linearVelocityY);
                break;
            case eCollisionDir.Right:
                if (_rig.linearVelocityX > 0)
                    _collisionedVelocity = new Vector2(-_rig.linearVelocityX, _rig.linearVelocityY);
                break;
        }

    }
    public void OnGround(bool isGround)
    {
        _isGround = isGround;
        if(isGround) _rig.linearVelocity = Vector2.zero;
    }
    private void PlayerMove()
    {
        if (_dir != Vector2.zero && _isGround)
            transform.Translate( _dir * Time.deltaTime * _moveSpeed);  
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;
        if(_collisionedVelocity != Vector2.zero)
        {
            _rig.linearVelocity = _collisionedVelocity;
            _collisionedVelocity = Vector2.zero;
        }
        if (_isPressJumpkey)
        {
            _jumpGage += _increaseGageValue * Time.fixedDeltaTime;

            //점프게이지 가득차면 점프 실행
            if(_jumpGage > _maximumGageValue)
            {
                _jumpGage = _maximumGageValue;
                JumpEnd();
            }
        }
        else
        {
            PlayerMove();
        }
        
    }
}
