using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerInvoker : MonoBehaviourPun
{
    [SerializeField] float _increaseGageValue = 0.2f;
    [SerializeField] float _minimumGageValue = 0.8f;
    [SerializeField] float _maximumGageValue = 5f;
    [SerializeField] float _moveSpeed = 3f;

    private Rigidbody2D _rig;
    private Animator _ani;

    private bool _isPressJumpkey;
    private float _jumpGage;
    private Vector2 _dir;

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
        _jumpGage = _minimumGageValue;
        _isPressJumpkey = true;
    }
    public void JumpEnd()
    {
        if (!_isPressJumpkey) return;
        _isPressJumpkey = false;

        _rig.AddForce((_dir + Vector2.up).normalized * _jumpGage, ForceMode2D.Impulse);
    }

    private void PlayerMove()
    {
        if (_dir != Vector2.zero)
            _rig.MovePosition(_dir * Time.deltaTime * _moveSpeed);  
    }

    private void Update()
    {
        if (_isPressJumpkey)
        {
            _jumpGage += _increaseGageValue * Time.deltaTime;

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
