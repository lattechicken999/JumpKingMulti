using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
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
