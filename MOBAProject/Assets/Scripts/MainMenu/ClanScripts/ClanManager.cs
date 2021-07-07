using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClanManager : MonoBehaviour
{
    [Header("All Game Objects")]
    public GameObject playerClan;
    public GameObject clanInfo;
    public GameObject clanCreate;
    public GameObject clanOwner;
    public GameObject invitePlayer;

    public GameObject content;
    public GameObject clanTextPrefab;
    public List<SelectClan> clansList;


    [Header("Buttons Data")]
    public GameObject clanCreateButton;
    public GameObject myClan;

    private string player;

    private void Awake()
    {
        player = DataBaseManager.Instance.Name;
    }

    private void OnEnable()
    {
        OnGetClans();
    }

    private void OnDisable()
    {
        for (int i = 0; i < content.transform.childCount; i++)
        {
            Destroy(content.transform.GetChild(i).gameObject);
        }
        clansList.Clear();
    }

    private void Update()
    {
        if (DataBaseManager.Instance.clanName != "")
        {
            clanCreateButton.SetActive(false);
            myClan.SetActive(true);
        }
        else
        {
            clanCreateButton.SetActive(true);
            myClan.SetActive(false);
        }
    }

    private void Start()
    {
        if (DataBaseManager.Instance.clanName != "")
        {
            clanCreateButton.SetActive(false);
            myClan.SetActive(true);
        }
        else
        {
            clanCreateButton.SetActive(true);
            myClan.SetActive(false);
        }
    }
    public void OnCreateClanClick()
    {
        if(DataBaseManager.Instance.clanName == "")
        {
            clanCreate.SetActive(true);
            clanInfo.SetActive(false);
            clanOwner.SetActive(false);
        }

    }

    public void OnMyClan()
    {
        if(DataBaseManager.Instance.Name == DataBaseManager.Instance.clanOwner)
        {
            clanInfo.SetActive(false);
            clanCreate.SetActive(false);
            playerClan.SetActive(false);
            clanOwner.SetActive(true);
            clanOwner.GetComponent<ClanOwner>().enabled = true;
        }
        else
        {
            clanOwner.SetActive(false);
            clanInfo.SetActive(false);
            clanCreate.SetActive(false);
            playerClan.SetActive(true);
            playerClan.GetComponent<PlayerClan>().enabled = true;
        }
    }


    public void OnPanelClose()
    {
        clanCreate.SetActive(false);
        clanInfo.SetActive(false);
        clanOwner.SetActive(false);
        playerClan.SetActive(false);
        invitePlayer.SetActive(false);
    }

    public void OnInvitePlayer()
    {
        clanOwner.GetComponent<ClanOwner>().enabled = false;
        clanOwner.SetActive(false);
        invitePlayer.SetActive(true);
    }

    public void OnGetClans()
    {
        for (int i = 0; i < clansList.Count; i++)
        {
            Destroy(clansList[i].gameObject);
        }
        clansList.Clear();
        StartCoroutine(GetAllClans());
    }

    public IEnumerator GetAllClans()
    {
        WWWForm webForm = new WWWForm();
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/getallclans.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    for(int i = 1; i < getChecker.Length-1;i++)
                    {
                        GameObject currentClan = Instantiate(clanTextPrefab);
                        currentClan.transform.SetParent(content.transform, false);
                        //currentClan.transform.parent = content.transform;
                        SelectClan currentSelect = currentClan.GetComponent<SelectClan>();
                        currentSelect.allClans = this;
                        currentSelect.clanName.text = getChecker[i];
                        clansList.Add(currentSelect);
                    }
                }
            }

        }
    }
}
