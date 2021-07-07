using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClanSearch : MonoBehaviour
{
    public InputField inputClan;
    public ClanManager clanManager;

    public GameObject searhButton;
    public GameObject updateButton;

    private void Start()
    {
        searhButton.SetActive(true);
        updateButton.SetActive(false);
    }
    public void OnSearchClan()
    {
        bool check = false;
        SelectClan foundClan = clanManager.clansList[0];
        if(inputClan.text != "")
        {
            for(int i = 0; i < clanManager.clansList.Count; i++)
            {
                if(clanManager.clansList[i].clanName.text == inputClan.text)
                {
                    check = true;
                    foundClan = clanManager.clansList[i];
                }
                else
                {
                    Destroy(clanManager.clansList[i].gameObject);
                }
            }
            if(check == false)
            {
                clanManager.clansList.Clear();
                GetClans();
            }
            else
            {
                clanManager.clansList.Clear();
                clanManager.clansList.Add(foundClan);
                searhButton.SetActive(false);
                updateButton.SetActive(true);
            }
        }
    }

    public void OnUpdateClans()
    {
        for (int i = 0; i < clanManager.clansList.Count; i++)
        {
            Destroy(clanManager.clansList[i].gameObject);
        }
        clanManager.clansList.Clear();
        GetClans();
        searhButton.SetActive(true);
        updateButton.SetActive(false);
    }
    public void GetClans()
    {
        StartCoroutine(clanManager.GetAllClans());
    }
}
