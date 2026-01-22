using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNickNameControl : MonoBehaviourPun
{
    [SerializeField] TextMeshProUGUI _nickNameText;

    public void SetNickName(string nickName)
    {
        if(photonView.IsMine)
        {
            _nickNameText.text = nickName;
        }
        else
        {
            _nickNameText.gameObject.SetActive(false);
        }
    }
}
