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
    public void LoadLoginNextScene()
    {
        SceneManager.LoadScene((int)eSceneNum.InGame);
    }
}
