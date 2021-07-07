using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class CreateClan : MonoBehaviour
{
    public ClanManager manager;
    public InputField clanName;
    public Text errorText;


    private void Start()
    {
        HideError();
    }
    public void OnCreateClan()
    {
        if(DataBaseManager.Instance.clanName == "")
        {
            StartCoroutine(ClanCreation());
            GameObject currentClan = Instantiate(manager.clanTextPrefab);
            currentClan.transform.SetParent(manager.content.transform, false);
            //currentClan.transform.parent = manager.content.transform;
            SelectClan currentSelect = currentClan.GetComponent<SelectClan>();
            currentSelect.allClans = manager;
            currentSelect.clanName.text = clanName.text;
            manager.clansList.Add(currentSelect);
        }
        else
        {
            errorText.text = "Вы уже состоите в клане.";
        }

        //manager.OnGetClans();
    }

    IEnumerator ClanCreation()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("clanName", clanName.text);
        webForm.AddField("username", DataBaseManager.Instance.Name);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/createClan.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //webRequest.SetRequestHeader("User-Agent", "runscope/0.1");
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
                    errorText.text = "Вы создали клан!";
                    errorText.gameObject.SetActive(true);
                    DataBaseManager.Instance.clanName = clanName.text;
                    DataBaseManager.Instance.clanOwner = DataBaseManager.Instance.Name;
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
            case "3":
                return "Это название уже занято. Повторите попытку.";
            case "4":
                return "Название должно быть в формате: Название";
            case "5":
                return "Ошибка в сохранении данных клана. Повторите попытку.";
        }
        return "";
    }
}
