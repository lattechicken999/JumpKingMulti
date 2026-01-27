using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNickNameControl : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI _nickNameText;

    private void Start()
    {
        if(!photonView.IsMine)
            _nickNameText.gameObject.SetActive(false);

    }
    public void SetNickName(string nickName)
    {
        if(photonView.IsMine)
        {
            _nickNameText.text = nickName;
        }
    }
}
