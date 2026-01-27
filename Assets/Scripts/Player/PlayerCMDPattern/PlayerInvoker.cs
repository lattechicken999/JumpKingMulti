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

    public Rigidbody2D RigidBody => _rig;
    private void Start()
    {
        _ani = GetComponent<Animator>();
        _rig = GetComponent<Rigidbody2D>();

        _dir = Vector2.zero; 
        _isPressJumpkey = false;
        _jumpGage = 0f;
    }

    public void SetMoveDir(Vector2 dir)
    {
        _dir = dir;
    }
    public void JumpStart()
    {
        if (_isGround || _rig.linearVelocity == Vector2.zero)
        {
            _jumpGage = _minimumGageValue;
            _isPressJumpkey = true;
        }
    }
    public void JumpEnd()
    {
        if (!_isPressJumpkey) return;
        _isPressJumpkey = false;

        //점프시 고정 해제
        _rig.constraints = RigidbodyConstraints2D.FreezeRotation;

        float gagePersent = Mathf.Clamp( _jumpGage / _maximumGageValue, _minimumGageValue/_maximumGageValue,0.7f);
        Vector2 jumpDir = _dir * (1 - gagePersent) + Vector2.up * (gagePersent);
        _rig.AddForce(jumpDir.normalized * _jumpGage, ForceMode2D.Impulse);
    }

    public void PlayerCollisionAct(Vector2 activeVelocity)
    {
        _rig.linearVelocity = activeVelocity;
    }
    public void OnGround(bool isGround)
    {
        _isGround = isGround;

        if (isGround)
        {
            _rig.linearVelocity = Vector2.zero;
            //땅에 닿아 있을 때는 장애물 취급
            //_rig.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            //_rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void PlayerMove()
    {
        if (_dir != Vector2.zero && (_isGround || _rig.linearVelocity == Vector2.zero))
            transform.Translate(_dir * Time.deltaTime * _moveSpeed);  
            //_rig.MovePosition(_rig.position + _dir * Time.deltaTime * _moveSpeed);
    }



    private void FixedUpdate()
    {
        if (!photonView.IsMine) return;

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
