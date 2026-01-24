using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FirebaseAuthManager : MonoBehaviour
{
    //firebase 관련 static 변수
    private static FirebaseAuth _auth;

    private static FirebaseUser _user =null;
    public static FirebaseUser User => _user;

    [Header("connect component")]
    [SerializeField] TMP_InputField _userEmailInpuField;
    [SerializeField] TMP_InputField _userPasswordField;
    [SerializeField] TMP_InputField _userNicknameField;
    [SerializeField] PanelOpenCloseAnimation _nicknamePanelAnimation;

    [Header("Prefebs")]
    [SerializeField] GameObject _userNickNameUI;

    private void Start()
    {
        _auth = FirebaseManager.Instance.Auth;
    }

    public void OnLogin()
    {
        StartCoroutine(LoginCoroutine(_userEmailInpuField.text,_userPasswordField.text));
    }
    IEnumerator LoginCoroutine(string email, string password)
    {
        Task<AuthResult> loginTask = _auth.SignInWithEmailAndPasswordAsync(email, password);
        //로그인 완료 까지 대기
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);

        if(loginTask.Exception != null)
        {
            //로그인 실패 처리

            Debug.LogWarning($"로그인 실패 : {loginTask.Exception}");

            //firebase 에서 제공해주는 error exception으로 변경
            FirebaseException firebaseEx = loginTask.Exception.GetBaseException() as FirebaseException;

            AuthError errorCode = (AuthError) firebaseEx.ErrorCode;

            string message = "";

            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Not Found Email";
                    break;
                default:
                    message = "Please contact us. whdtn218@gmail.com";
                    break;
            }
            AlertManager.Instance.CallAlertUI(message);
        }
        else
        {
            _user = loginTask.Result.User;
            FirebaseDbManager.Instance.LoadUserInfo();
            SceneChangeManager.Instance.LoadLobbyScene();
        }
    }
    public void OnResistor()
    {
        StartCoroutine(ResisterCoroutine(_userEmailInpuField.text, _userPasswordField.text));
    }
    IEnumerator ResisterCoroutine(string email, string password)
    {
        Task<AuthResult> resistTask = _auth.CreateUserWithEmailAndPasswordAsync (email, password);
        
        //로그인 완료 까지 대기
        yield return new WaitUntil(predicate: () => resistTask.IsCompleted);

        if (resistTask.Exception != null)
        {
            //로그인 실패 처리

            Debug.LogWarning($"회원 가입 실패 : {resistTask.Exception}");

            //firebase 에서 제공해주는 error exception으로 변경
            FirebaseException firebaseEx = resistTask.Exception.GetBaseException() as FirebaseException;

            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "";

            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WeakPassword:
                    message = "Weak Password";
                    break;
                case AuthError.EmailAlreadyInUse:
                    message = "Email Already In Use";
                    break;
                default:
                    message = "Please contact us. whdtn218@gmail.com";
                    break;
            }
            AlertManager.Instance.CallAlertUI(message);
        }
        else
        {
            _user = resistTask.Result.User;
            if (_user != null)
            {
                //닉네임 등록 창 활성화
                _nicknamePanelAnimation.Show();
            }
        }
    }

    public void ComplateSetUserName()
    {
        StartCoroutine(SetUserProfile());
    }
    IEnumerator SetUserProfile()
    {
        UserProfile profile = new UserProfile { DisplayName = _userNicknameField.text };
        Task profileTask = _user.UpdateUserProfileAsync(profile);
        yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

        if (profileTask.Exception != null)
        {
            Debug.LogWarning($"닉네임 설정 실패 : {profileTask.Exception}");
            FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            AlertManager.Instance.CallAlertUI("닉네임 설정에 실패하였습니다.");
        }
        else
        {
            FirebaseDbManager.Instance.SaveUserInfo();
            SceneChangeManager.Instance.LoadLobbyScene();
        }
    }

    public void OnClickCancleNicknameWindow()
    {
        StartCoroutine(CancleRegisterUser());
    }
    private IEnumerator CancleRegisterUser()
    {
        var delUserTask = _user.DeleteAsync();
        yield return new WaitUntil(() => delUserTask.IsCompleted);

        if(delUserTask.Exception != null)
        {
            Debug.LogWarning($"계정 생성 취소 - 계정 삭제 실패 {delUserTask.Exception}");
        }    
        _nicknamePanelAnimation.HIde();
    }
}
