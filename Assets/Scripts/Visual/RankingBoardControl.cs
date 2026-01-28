using ExitGames.Client.Photon.StructWrapping;
using System.Collections;
using UnityEngine;

public class RankingBoardControl : MonoBehaviour ,IGameClearOpserver
{
    [SerializeField] GameObject _rankListElementPrefeb;
    [SerializeField] GameObject _clearPanel;
    [SerializeField] Transform _RankingList;
    [SerializeField] RankingInfoControl _myRankInfo;
    private void Start()
    {
        InGameManager.Instance.RegistGameClearSub(this);
        _clearPanel.SetActive(false);
    }
    private void OnDestroy()
    {
        InGameManager.Instance.UnregistGameClearSub(this);
    }
    public void GameClear()
    {
        _clearPanel.SetActive(true);
        StartCoroutine(VisualizeRankingInfo());

    }
    private IEnumerator VisualizeRankingInfo()
    {
        yield return FirebaseDbManager.Instance.StartGetRankInfo();
        var rankingList = FirebaseDbManager.Instance.RankList;
        if (rankingList == null) yield break;
        for(int i = 0; i < rankingList.Count; i++)
        {
            var rankingInst = Instantiate(_rankListElementPrefeb, _RankingList,true);
            rankingInst.GetComponent<RankingInfoControl>().SetRankInfo((i + 1).ToString(), rankingList[i].NickName, rankingList[i].BestClearTime);
            if (rankingList[i].NickName == FirebaseAuthManager.User.DisplayName)
            {
                _myRankInfo.SetRankInfo((i + 1).ToString(), rankingList[i].NickName, rankingList[i].BestClearTime);
            }
        }
    }
}
