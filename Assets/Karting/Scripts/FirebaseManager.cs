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
    public int xp; 
    public string username;

    // //Login variables
    // [Header("Login")]
    // public TMP_InputField emailLoginField;
    // public TMP_InputField passwordLoginField;
    // public TMP_Text warningLoginText;
    // public TMP_Text confirmLoginText;

    // //Register variables
    // [Header("Register")]
    // public TMP_InputField usernameRegisterField;
    // public TMP_InputField emailRegisterField;
    // public TMP_InputField passwordRegisterField;
    // public TMP_InputField passwordRegisterVerifyField;
    // public TMP_Text warningRegisterText;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
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
        // xp = 50;
        StartCoroutine(UpdateXp(xp));
    }
     public void LoadDataButton()
    {
        StartCoroutine(LoadUserData());
    }

    public void LoginButton()
    {
        Debug.Log("Logging in 1");

        var email = "abc@gmail.com";
        var pwd = "123456789";
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(email, pwd));
    }

    private IEnumerator Login(string _email, string _password)
    {
        Debug.Log("Logging in");

        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        Debug.Log("LoginTask: " + LoginTask);

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

    private IEnumerator UpdateXp(int _xp)
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
            //Xp is now updated
            Debug.Log("xp updated");
        }
    }

    private IEnumerator LoadUserData()
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
}
