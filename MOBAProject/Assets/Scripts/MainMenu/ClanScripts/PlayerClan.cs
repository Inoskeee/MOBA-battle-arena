using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerClan : MonoBehaviour
{
    public Text clanName;
    public Text clanOwner;
    public Text clanRaiting;

    public GameObject content;
    public GameObject playerPrefab;


    private void OnEnable()
    {
        OnPlayerClanSelect();
    }

    private void OnDisable()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
    }

    public void OnLeaveButton()
    {
        StartCoroutine(ClanLeave());
    }
    void OnPlayerClanSelect()
    {
        StartCoroutine(GetPlayerClan());
    }
    IEnumerator GetPlayerClan()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("clanName", DataBaseManager.Instance.clanName);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/getPlayerClanInfo.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    clanName.text = getChecker[1];
                    clanRaiting.text = getChecker[2];
                    clanOwner.text = getChecker[3];
                    for (int i = 4; i < getChecker.Length - 1; i++)
                    {
                        GameObject currentMember = Instantiate(playerPrefab);
                        currentMember.transform.SetParent(content.transform, false);
                        //currentMember.transform.parent = content.transform;
                        currentMember.GetComponentInChildren<Text>().text = getChecker[i];
                    }
                }
            }

        }
    }

    IEnumerator ClanLeave()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("clanName", DataBaseManager.Instance.clanName);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/leaveClan.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);


            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (webRequest.downloadHandler.text == "0")
                {
                    DataBaseManager.Instance.clanName = "";
                    DataBaseManager.Instance.clanOwner = "";
                    gameObject.SetActive(false);
                }
            }
        }
    }

}
