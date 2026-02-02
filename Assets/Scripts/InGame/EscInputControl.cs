using UnityEngine;
using UnityEngine.InputSystem;

public class EscInputControl : MonoBehaviour
{
    [SerializeField] GameObject _setting;
    private InputAction _escAction;
    private bool _activeTrigger = false;
    private void Awake()
    {
        _escAction = InputSystem.actions["Setting"];
    }

    private void Start()
    {
        _escAction.started += (ctx) =>
            _setting.SetActive(!_activeTrigger);

    }
    private void OnDestroy()
    {
        _escAction.started -= (ctx) =>
            _setting.SetActive(!_activeTrigger);

    }
}
