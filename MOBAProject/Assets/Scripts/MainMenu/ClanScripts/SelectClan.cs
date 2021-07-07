using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Networking;

public class SelectClan : MonoBehaviour, IPointerClickHandler
{
    public ClanManager allClans;
    public ClanInfo clanInfo;
    public Text clanName;

    private Image background;

    public Sprite nonSelectedImg;
    public Sprite selectedImg;
    private void Start()
    {
        background = GetComponent<Image>();
        clanInfo = allClans.clanInfo.GetComponent<ClanInfo>();
        clanInfo.enabled = true;
        background.sprite = nonSelectedImg;
        //background.color = Color.clear;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        allClans.clanInfo.SetActive(false);
        allClans.clanOwner.SetActive(false);
        allClans.playerClan.SetActive(false);
        allClans.clanCreate.SetActive(false);
        for (int i = 0; i < allClans.clansList.Count; i++)
        {
            allClans.clansList[i].background.sprite = nonSelectedImg;
            //allClans.clansList[i].background.color = Color.clear;
        }
        background.sprite = selectedImg;
        //background.color = Color.blue;
        OnSelectedClan();
        CheckerClan();
    }

    void CheckerClan()
    {
        if (DataBaseManager.Instance.clanOwner == DataBaseManager.Instance.Name && DataBaseManager.Instance.clanName == clanName.text)
        {
            allClans.clanOwner.SetActive(true);
            allClans.clanOwner.GetComponent<ClanOwner>().enabled = true;
        }
        else if (clanName.text == DataBaseManager.Instance.clanName)
        {
            allClans.playerClan.SetActive(true);
            allClans.playerClan.GetComponent<PlayerClan>().enabled = true;
        }
        else
        {
            allClans.clanInfo.SetActive(true);
            allClans.playerClan.GetComponent<PlayerClan>().enabled = false;
        }
    }


    void OnSelectedClan()
    {
        StartCoroutine(GetSelectedClan());
    }
    IEnumerator GetSelectedClan()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("clanName", clanName.text);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/selectClan.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    clanInfo.clanName.text = getChecker[1];
                    clanInfo.clanRaiting.text = getChecker[2];
                    clanInfo.clanOwner.text = getChecker[3];
                }
            }

        }
    }
}
