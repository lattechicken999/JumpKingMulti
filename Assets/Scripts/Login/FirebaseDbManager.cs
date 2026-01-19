
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
public class FirebaseDbManager : Singleton<FirebaseDbManager>
{
    private DatabaseReference _dbRef;
    private DataSnapshot _userInfo;

    static private DatabaseReference _userInfoRef;
    static public DatabaseReference _UserInfoRef => _userInfoRef;

    private void Start()
    {
        _dbRef = FirebaseManager.Instance.Database;
    }

    /// <summary>
    /// DB에서 유저 정보 가져오는 함수
    /// Nickname을 고유 ID 처럼 관리함
    /// 어짜피 이메일 id의 고유성은 Auth에서 관리함
    /// </summary>
    /// <returns></returns>
    private IEnumerator GetUserInfo()
    {
        var checkNicknameTask = _dbRef.Child("Nicknames").GetValueAsync();
        yield return new WaitUntil(() => checkNicknameTask.IsCompleted);

        if(checkNicknameTask.Exception !=null)
        {
            Debug.LogWarning($"DB 읽기 실패 : {checkNicknameTask.Exception}");

        }
        _userInfo = checkNicknameTask.Result;
    }


    
}
