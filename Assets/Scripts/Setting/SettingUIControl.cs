using UnityEngine;

public class SettingUIControl : MonoBehaviour
{
    [SerializeField] GameObject _settingUiPrefeb;

    public void OnSettingUI()
    {
        Instantiate(_settingUiPrefeb);
    }
}
