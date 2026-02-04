using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviourPunCallbacks,IPunObservable,IGameClearOpserver
{
    [SerializeField] CommandRecorder _recorder;
    [SerializeField] PlayerInvoker _player;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    private SpriteRenderer _playerRenderer;
    float lastCommandTime;

    public PlayerInvoker Player => _player;

    public void EnablePlayerInputHandler()
    {
        if (!photonView.IsMine) return;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJump;
    }
    public void DisablePlayerInputHandler()
    {
        if (!photonView.IsMine) return;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;
        _jumpAction.performed -= OnJump;
        _jumpAction.canceled -= OnJump;
    }

    private void Awake()
    {
        _moveAction = InputSystem.actions["MoveDirection"];
        _jumpAction = InputSystem.actions["Jump"];
        _playerRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        EnablePlayerInputHandler();
    }

    public override void OnDisable()
    {
        DisablePlayerInputHandler();
    }

    [PunRPC]
    private void SetSpriteRendererFlip(float dirx)
    {
        if (dirx < 0)
        {
            _playerRenderer.flipX = true;
        }
        else if(dirx > 0)
        {
            _playerRenderer.flipX = false;
        }
    }
    private void OnMove(InputAction.CallbackContext ctx)
    {
        Vector2 dir = ctx.ReadValue<Vector2>();
        photonView.RPC("SetSpriteRendererFlip", RpcTarget.All, dir.x);
        CommandExcute(new MoveCommand(_player, dir));
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        if(!ctx.ReadValueAsButton())
            EffectSoundManager.Instance.PlayJumpSound();
        CommandExcute(new JumpCommand(_player,ctx.ReadValueAsButton()));
    }
    public void CommandExcute(IPlayerCommand command)
    {
        float now = Time.time;
        float gap = now - lastCommandTime;
        //if (gap > 0.01f)
        //{
        //    _recorder.Record(new WaitCommand(gap));
        //}
        //_recorder.Record(command);
        lastCommandTime = now;
        command.Execute();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        
    }

    public void GameClear()
    {
        if(photonView.IsMine)
        {
            //게임이 클리어 되면 플레이어 조작 금지
            _moveAction.performed -= OnMove;
            _moveAction.canceled -= OnMove;
            _jumpAction.performed -= OnJump;
            _jumpAction.canceled -= OnJump;
        }
    }
}
