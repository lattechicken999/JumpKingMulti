
using Firebase.Database;
using System.Collections;
using UnityEngine;

////////////////////////////////////////////////////////////////////////////////////////////////
/// Db tree
/// Nicknames
/// {
///     [Nickname] :
///     {
///         RecordTime :
///         {
///             record time
///         }
///     }
/// }
///             
////////////////////////////////////////////////////////////////////////////////////////////////
public class UserData
{
    public string NickName;
    public string Email;
    public long BestClea;
}
public class FirebaseDbManager : Singleton<FirebaseDbManager>
{
    private DatabaseReference _dbRef;
    private DataSnapshot _userInfo;

    static private DatabaseReference _userInfoRef;
    static public DatabaseReference _UserInfoRef => _userInfoRef;

    private long _userClearTime;

    private void Start()
    {
        _dbRef = FirebaseManager.Instance.Database;
        _userClearTime = 0;
    }

    public string GetUserNickName()
    {
        if (_userInfo == null) return string.Empty;
        if(!_userInfo.Exists) return string.Empty;

        return _userInfo.Key;
    }
    public void LoadUserInfo()
    {
        StartCoroutine(GetUserInfo());
        StartCoroutine(GetUserBestClearTime());
    }

    /// <summary>
    /// DB에서 유저 정보 가져오는 함수
    /// Nickname을 고유 ID 처럼 관리함
    /// 어짜피 이메일 id의 고유성은 Auth에서 관리함
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetUserInfo()
    {
        var checkNicknameTask = _dbRef.Child("Nicknames")
                                                        .Child(FirebaseAuthManager.User.DisplayName)
                                                        .GetValueAsync();
        yield return new WaitUntil(() => checkNicknameTask.IsCompleted);

        if(checkNicknameTask.Exception !=null)
        {
            Debug.LogWarning($"DB 읽기 실패 : {checkNicknameTask.Exception}");
        }
        else
            _userInfo = checkNicknameTask.Result;
    }

    public void SaveUserInfo()
    {
        StartCoroutine(SetUserInfo());
    }
    private IEnumerator SetUserInfo()
    {
        var SetNickNameDataTask = _dbRef.Child("Nicknames")
                                                            .SetValueAsync(FirebaseAuthManager.User.DisplayName);
        yield return new WaitUntil(()=> SetNickNameDataTask.IsCompleted);

        if(SetNickNameDataTask.Exception != null)
        {
            Debug.LogWarning($"DB 에 닉네임 저장 실패 : {SetNickNameDataTask.Exception}");
        }
        else
        {
            var SetEmailTask = _dbRef.Child("Nicknames")
                                                   .Child(FirebaseAuthManager.User.DisplayName)
                                                   .Child("Email")
                                                   .SetValueAsync(FirebaseAuthManager.User.Email);
            yield return new WaitUntil( ()=> SetEmailTask.IsCompleted);

            if(SetEmailTask.Exception != null)
            {
                Debug.LogWarning($"DB에 이메일 저장 실패 : {SetEmailTask.Exception}");
            }
            else
            {
                //저장 하면 다시 로드함
                LoadUserInfo();
            }
        }
    }

    public void SaveUserClearTime(long timeStamp)
    {
        if(_userClearTime == 0 || (_userClearTime > timeStamp))
        {
            //기록 갱신
            _userClearTime = timeStamp;
            StartCoroutine(SetUserBestClearTime());
        }
        else
        {
            //pass
        }
    }
    private IEnumerator SetUserBestClearTime()
    {
        var SetBestClearTimeTask = _dbRef.Child("Nicknames")
                                                    .Child(FirebaseAuthManager.User.DisplayName)
                                                    .Child("BestClearTime")
                                                    .SetValueAsync(_userClearTime);
        yield return new WaitUntil(() => SetBestClearTimeTask.IsCompleted);

        if (SetBestClearTimeTask.Exception != null)
        {
            Debug.LogWarning($"DB에서 시간 기록 저장 실패 : {SetBestClearTimeTask.Exception}");
        }
    }
    private IEnumerator GetUserBestClearTime()
    {
        var GetBestClearTimeTask = _dbRef.Child("Nicknames")
                                                    .Child(FirebaseAuthManager.User.DisplayName)
                                                    .Child("BestClearTime")
                                                    .GetValueAsync();
        yield return new WaitUntil(() => GetBestClearTimeTask.IsCompleted);

        if(GetBestClearTimeTask.Exception != null)
        {
            Debug.LogWarning($"DB에서 시간 기록 가져오기 실패 : {GetBestClearTimeTask.Exception}");
        }
        else
        {
            if (GetBestClearTimeTask.Result.Exists)
                _userClearTime = (long)GetBestClearTimeTask.Result.Value;
            else
                _userClearTime = 0;
        }
    }

}
