using TMPro;
using UnityEngine;

public class RankingInfoControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _rankText;
    [SerializeField] TextMeshProUGUI _nicknameText;
    [SerializeField] TextMeshProUGUI _timeText;

    public void SetRankInfo(string rank, string nickname,long time)
    {
        _rankText.text = rank;
        _nicknameText.text = nickname;
        string timeText = $"{(time/1000)/60} : {(time/1000)%60}.{time%1000}";
        _timeText.text = timeText;
    }
}
