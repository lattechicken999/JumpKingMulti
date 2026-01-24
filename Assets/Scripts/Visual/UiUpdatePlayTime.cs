using TMPro;
using UnityEngine;

public class UiUpdatePlayTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playingTimeText;
    // Update is called once per frame
    void Update()
    {
        _playingTimeText.text = $"{(int)PlayerTimeManager.PlayTime / 60}:{(int)PlayerTimeManager.PlayTime % 60:D2}.{(int)(PlayerTimeManager.PlayTime*1000 )%1000:D3}";
    }
}
