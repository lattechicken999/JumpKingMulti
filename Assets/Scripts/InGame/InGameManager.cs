using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
public class InGameManager : Singleton<InGameManager>
{
    private List<IGameClearOpserver> _gameClearSub = new List<IGameClearOpserver>();

    private void OnDestroy()
    {
        UnregistAll();
    }

    public void UnregistAll()
    {
        _gameClearSub.Clear();
    }
    public void RegistGameClearSub(IGameClearOpserver sub)
    {
        if (_gameClearSub.Contains(sub))
            return;
        _gameClearSub.Add(sub);
    }

    public void UnregistGameClearSub(IGameClearOpserver sub)
    {
        if (_gameClearSub.Contains(sub))
            _gameClearSub.Remove(sub);
    }
    public void SendToSubGameClear()
    {
        foreach(var s in _gameClearSub)
        {
            s.GameClear();
        }
    }

}
