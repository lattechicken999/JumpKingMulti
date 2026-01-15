using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class FirebaseAuthManager : MonoBehaviour
{
    //firebase 관련 static 변수
    private static FirebaseAuth _auth;
    public static FirebaseAuth Auth => _auth;

    private static FirebaseUser _user;
    public static FirebaseUser User => _user;

    private static DatabaseReference _dbRef;
    public static DatabaseReference Database => _dbRef;


    [Header("connect component")]
    [SerializeField] TMP_InputField _userEmailInpuField;
    [SerializeField] TMP_InputField _userPasswordField;
    [SerializeField] TMP_InputField _userNickname;

    [Header("Alert Prefeb")]
    [SerializeField] GameObject _AlertObjectPrefeb;
    [SerializeField] GameObject _canvas;

    private IEnumerator Start()
    {
        var task =  Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(()=>task.IsCompleted);

        var dependencyStatus = task.Result;
        if (dependencyStatus == Firebase.DependencyStatus.Available)
        {
            _auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
            _dbRef = FirebaseDatabase.DefaultInstance.RootReference;
        }
        else
        {
            Debug.LogError("Firebase authentication failed");
        }
    }

    private void CallAlertUI(string message)
    {
        var alertObject = Instantiate(_AlertObjectPrefeb, _canvas.transform);
        AlertControl alertControl = alertObject.GetComponent<AlertControl>();
        alertControl.UpdateTextMessage(message);
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
            CallAlertUI(message);
        }
        else
        {
            _user = loginTask.Result.User;
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
            CallAlertUI(message);
        }
        else
        {
            _user = resistTask.Result.User;
            if (_user != null)
            {
                UserProfile profile = new UserProfile { DisplayName = _userNickname.text };
                Task profileTask = _user.UpdateUserProfileAsync(profile);
                yield return new WaitUntil(predicate: () => profileTask.IsCompleted);

                if (profileTask.Exception != null)
                {
                    Debug.LogWarning($"닉네임 설정 실패 : {profileTask.Exception}");
                    FirebaseException firebaseEx = profileTask.Exception.GetBaseException() as FirebaseException;
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    CallAlertUI( "닉네임 설정에 실패하였습니다.");
                }
                else
                {
                    CallAlertUI( "생성 완료, 반갑습니다." + _user.DisplayName + "님");
                }
            }
        }
    }
}
