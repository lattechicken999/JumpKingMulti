using UnityEngine;

public class SettingUIControl : MonoBehaviour
{
    [SerializeField] GameObject _settingUi;

    public void OnSettingUI()
    {
        _settingUi.SetActive(true);
    }
    public void OnCloseUI()
    {
        _settingUi.SetActive(false);
    }
}
