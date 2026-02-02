using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SettingUIControl : MonoBehaviour
{
    [SerializeField] GameObject _settingUi;

    private void OnEnable()
    {
        InputSystem.actions["MoveDirection"].Disable();
        InputSystem.actions["Jump"].Disable();
    }
    private void OnDisable()
    {
        InputSystem.actions["MoveDirection"].Enable();
        InputSystem.actions["Jump"].Enable();
    }
    public void OnSettingUI()
    {
        _settingUi.SetActive(true);
    }
    public void OnCloseUI()
    {
        _settingUi.SetActive(false);
    }
}
