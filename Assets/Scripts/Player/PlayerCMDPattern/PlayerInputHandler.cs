using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
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
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _jumpAction.performed += OnJump;
        _jumpAction.canceled += OnJump;
    }

    private void OnDisable()
    {
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
}
