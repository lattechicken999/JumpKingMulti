using Unity.VisualScripting;
using UnityEngine;

public class AlertManager : Singleton<AlertManager>
{
    [Header("Alert Prefeb")]
    [SerializeField] GameObject _AlertObjectPrefeb;
    public void CallAlertUI(string message)
    {
        var alertObject = Instantiate(_AlertObjectPrefeb);
        AlertControl alertControl = alertObject.GetComponent<AlertControl>();
        alertControl.UpdateTextMessage(message);
    }
}
