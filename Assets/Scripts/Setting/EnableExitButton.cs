using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnableExitButton : MonoBehaviour
{
    [SerializeField] TMP_InputField _oathInputField;
    [SerializeField] Button _ExitGame;

    private void Awake()
    {
        _ExitGame.interactable = false;
    }
    public void OnChangeInputField()
    {
        if(_oathInputField.text == "좋은 에셋이 있다면 반드시 공유 하겠습니다.")
        {
            _ExitGame.interactable = true;
        }
        else
        {
            _ExitGame.interactable = false;
        }
    }
}
