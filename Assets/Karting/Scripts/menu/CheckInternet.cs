using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CheckInternet : MonoBehaviour
{
    [SerializeField] TMP_Text loadingText;
    [SerializeField] TMP_Text connectionError;
    [SerializeField] Button statisticButton;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckInternetConnection());
        
    }
    void Update(){
        StartCoroutine(CheckInternetConnection());
    }
    IEnumerator CheckInternetConnection()
    {
        UnityWebRequest request = new UnityWebRequest("http://google.com");
        yield return request.SendWebRequest();
        if(request.error != null)
        {
            loadingText.gameObject.SetActive(false);
            connectionError.gameObject.SetActive(true);
            statisticButton.gameObject.SetActive(true);
        }
        else
        {
            loadingText.gameObject.SetActive(true);
            connectionError.gameObject.SetActive(false);
            statisticButton.gameObject.SetActive(true);
        }
    }
    public void TryAgain()
    {

    }

}
