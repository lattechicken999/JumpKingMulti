using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviourPunCallbacks,IPunObservable,IGameClearOpserver
{
    [SerializeField] CommandRecorder _recorder;
    [SerializeField] PlayerInvoker _player;
    private InputAction _moveAction;
    private InputAction _jumpAction;

    float lastCommandTime;

    public PlayerInvoker Player => _player;

    private void Awake()
    {
        _moveAction = InputSystem.actions["MoveDirection"];
        _jumpAction = InputSystem.actions["Jump"];
    }
    private void Start()
    {
        if (!photonView.IsMine) return;
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJump;
    }

    public override void OnDisable()
    {
        if (!photonView.IsMine) return;
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;
        _jumpAction.performed -= OnJump;
        _jumpAction.canceled -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        RecordAndExcute(new MoveCommand(_player, ctx.ReadValue<Vector2>()));
    }
    private void OnJump(InputAction.CallbackContext ctx)
    {
        RecordAndExcute(new JumpCommand(_player,ctx.ReadValueAsButton()));
    }
    public void RecordAndExcute(IPlayerCommand command)
    {
        float now = Time.time;
        float gap = now - lastCommandTime;
        if (gap > 0.01f)
        {
            _recorder.Record(new WaitCommand(gap));
        }
        _recorder.Record(command);
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
