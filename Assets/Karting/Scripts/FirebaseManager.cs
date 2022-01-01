using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;
    public DatabaseReference DBreference;

    [Header("Score")]
    public int position;
    public int xp;
    public bool won;
    public string mapName;
    public List<ScoreElement> ScoresList = new List<ScoreElement>();

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveDataButton()
    {
        StartCoroutine(UpdateXp(xp));
    }
    public void LoadDataButton()
    {
        StartCoroutine(LoadUserScores());
    }
    public void SaveScoreButton()
    {
        StartCoroutine(SaveScore(won, position, mapName, xp));
    }
    public void LoginButton()
    {
        var email = "abc@gmail.com";
        var pwd = "123456789";

        StartCoroutine(Login(email, pwd));
    }

    public IEnumerator Login(string _email, string _password)
    {
        Debug.Log("Logging in");
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
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
                    message = "Account does not exist";
                    break;
            }
            // warningLoginText.text = message;
            Debug.LogWarning(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            // warningLoginText.text = "";
            // confirmLoginText.text = "Logged In";
        }
    }

    public IEnumerator UpdateXp(int _xp)
    {
        //Set the currently logged in user xp
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("xp").SetValueAsync(_xp);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("xp updated");
        }
    }

    public IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            Debug.Log("nothing to load from users");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;
            var xpStr = snapshot.Child("xp").Value.ToString();

            Debug.Log("Data has been retrieved");
            Debug.Log("xp = " + xpStr);
        }
    }

    // ---------------GAMES Table------------------

    public IEnumerator SaveScore(bool _won, int _pos, string _mapName, int _xp)
    {
        ScoreElement score = new ScoreElement(_won, _pos, _mapName, _xp);
        // string json = JsonUtility.ToJson(score);
        string json = score.ToJSON();
        var DBTask = DBreference.Child("scores").Child(User.UserId).Push().SetRawJsonValueAsync(json);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);
        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            Debug.Log("score saved");
        }
    }

    public IEnumerator LoadUserScores()
    {
        var DBTask = DBreference.Child("scores").Child(User.UserId).GetValueAsync();
        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                var json = childSnapshot.GetRawJsonValue();
                var scoreElement = JsonUtility.FromJson<ScoreElement>(json);
                ScoresList.Add(scoreElement);
            }

            //Go to scoareboard screen
            // UIManager.instance.ScoreboardScreen();
        }
    }

    public List<ScoreElement> GetUserScores()
    {
        StartCoroutine(LoadUserScores());
        return ScoresList;
    }


}
