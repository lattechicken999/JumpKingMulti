using UnityEngine;

public class PlayerTimeManager : MonoBehaviour,IGameClearOpserver
{

    static float _playTime = 0;
    public static float PlayTime => _playTime;

    bool _isPlaying;

    private void Start()
    {
        _playTime = 0;
        _isPlaying = true;
        InGameManager.Instance.RegistGameClearSub(this);
    }
    private void OnDestroy()
    {
        InGameManager.Instance.UnregistGameClearSub(this);
    }
    void Update()
    {
        if(_isPlaying)
            _playTime += Time.deltaTime;
    }

    public void GameClear()
    {
        _isPlaying = false;
    }
}
