using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInClan : MonoBehaviour
{
    public Text playerName;
    public Button deleteButton;

    private void Start()
    {
        if (playerName.text == DataBaseManager.Instance.Name)
        {
            deleteButton.gameObject.SetActive(false);
        }
    }
    public void OnMemberRetirement()
    {
        StartCoroutine(ClanLeave());
    }
    IEnumerator ClanLeave()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", playerName.text);
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
                    Destroy(gameObject);

                    GameObject.FindObjectOfType<ClanOwner>().OnPlayerClanSelect();
                }
            }
        }
    }
}
