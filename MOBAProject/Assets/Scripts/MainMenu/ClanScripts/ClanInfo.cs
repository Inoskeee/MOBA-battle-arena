using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ClanInfo : MonoBehaviour
{
    public Text clanName;
    public Text clanOwner;
    public Text clanRaiting;

    public Text errorText;

    private void Start()
    {
        HideError();
    }

    private void OnDisable()
    {
        HideError();
    }
    public void OnJoinButton()
    {
        if(DataBaseManager.Instance.clanName == "")
        {
            StartCoroutine(AddPlayerInClan());
        }
        else
        {
            errorText.color = Color.red;
            errorText.text = "Вы уже состоите в клане";
            errorText.gameObject.SetActive(true);
        }
    }

    IEnumerator AddPlayerInClan()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("clanName", clanName.text);
        webForm.AddField("username", DataBaseManager.Instance.Name);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/joinClan.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);


            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                errorText.text = webRequest.error;
                errorText.enabled = true;
            }
            else
            {
                if (webRequest.downloadHandler.text == "0")
                {
                    errorText.color = Color.green;
                    errorText.text = "Вы вступили в клан!";
                    errorText.gameObject.SetActive(true);
                    DataBaseManager.Instance.clanName = clanName.text;
                }
                else
                {
                    errorText.color = Color.red;
                    errorText.text = ErrorText(webRequest.downloadHandler.text);
                    errorText.gameObject.SetActive(true);
                }
            }
        }
    }

    public void HideError()
    {
        errorText.gameObject.SetActive(false);
    }

    public static string ErrorText(string errorCode)
    {
        switch (errorCode)
        {
            case "1":
                return "Ошибка соединения с сервером. Повторите попытку.";
            case "2":
                return "Ошибка запроса на сервер. Повторите попытку.";
            case "5":
                return "Ошибка в сохранении данных игрока. Повторите попытку.";
        }
        return "";
    }
}
