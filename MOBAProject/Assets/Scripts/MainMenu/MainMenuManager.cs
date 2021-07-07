using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{
    public Text nameUser;
    public Text startInfo;

    public GameObject lobby;
    public GameObject inventory;
    public GameObject clans;
    public Button playButton;
    public Button inventoryButton;
    public Button clanButton;

    public Text raiting;

    void Awake()
    {
        nameUser.text = DataBaseManager.Instance.Name;
        raiting.text = "Счет: " + DataBaseManager.Instance.Score.ToString();
    }

    public void OnLobby()
    {
        playButton.enabled = false;
        lobby.SetActive(true);
    }

    public void OnLoadInventory()
    {
        startInfo.enabled = false;
        clans.GetComponent<ClanManager>().enabled = false;
        clans.SetActive(false);
        inventory.SetActive(true);
    }

    public void OnCloseInventory()
    {
        inventory.SetActive(false);
        startInfo.enabled = true;
        CallSaveDataPalyer();
    }

    public void OnLoadClan()
    {
        startInfo.enabled = false;
        inventory.SetActive(false);
        clans.GetComponent<ClanManager>().enabled = true;
        clans.SetActive(true);
    }
    public void OnCloseClan()
    {
        clans.SetActive(false);
        ClanManager allClans = clans.GetComponent<ClanManager>();
        allClans.clanCreate.SetActive(false);
        allClans.playerClan.SetActive(false);
        allClans.clanOwner.GetComponent<ClanOwner>().enabled = false;
        allClans.clanOwner.SetActive(false);
        allClans.clanInfo.GetComponent<ClanInfo>().enabled = false;
        allClans.clanInfo.SetActive(false);

        allClans.enabled = false;
        startInfo.enabled = true;
    }

    public void OnExitGame()
    {
        Application.Quit();
    }

    public void OnLoadGame()
    {
        SceneManager.LoadScene(2);
    }

    void CallSaveDataPalyer()
    {
        StartCoroutine(SaveDataPlayer());
    }
    IEnumerator SaveDataPlayer()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("head", DataBaseManager.Instance.HeadId);
        webForm.AddField("body", DataBaseManager.Instance.BodyId);
        webForm.AddField("leg", DataBaseManager.Instance.LegId);
        webForm.AddField("sword", DataBaseManager.Instance.SwordId);
        webForm.AddField("bow", DataBaseManager.Instance.BowId);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/savePlayerData.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);

            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
        }
    }

}
