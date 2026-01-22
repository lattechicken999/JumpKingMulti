using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using UnityEngine;

public class FirebaseManager : Singleton<FirebaseManager>
{
    private DatabaseReference _dbRef;
    public DatabaseReference Database => _dbRef;
    private FirebaseAuth _auth;
    public FirebaseAuth Auth => _auth;
    private IEnumerator Start()
    {
        var task = Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(() => task.IsCompleted);

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

}
