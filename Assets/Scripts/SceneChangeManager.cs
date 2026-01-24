using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : Singleton<SceneChangeManager>
{
    /// 씬 컨트롤
    public void LoadTitleNextScene()
    {
        if (FirebaseAuthManager.User == null)
            SceneManager.LoadScene((int)eSceneNum.Login);
        else
            SceneManager.LoadScene((int)eSceneNum.InGame);
    }
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene((int)eSceneNum.Lobby);
    }
    public void LoadInGameScene()
    {
        SceneManager.LoadScene((int)eSceneNum.InGame);
    }


}
