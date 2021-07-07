using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectBooster : MonoBehaviour
{
    public ItemList items;
    public List<CheckItem> booster;

    private void Start()
    {
        for (int i = 0; i < booster.Count; i++)
        {
            if (DataBaseManager.Instance.BoosterId - 1 == i)
            {
                booster[i].select.gameObject.SetActive(false);
                booster[i].panel.gameObject.SetActive(true);
                booster[i].countText.text = DataBaseManager.Instance.Count.ToString();
            }
            booster[i].id = i + 1;
            booster[i].OnBoost();
            ListenOnButton(i);
            ListenOnPlusButton(i);
            ListenOnMinusButton(i);
        }
    }

    public void OnSelectClick(CheckItem item)
    {
        CallAllBoostersDelete();
        for (int i = 0; i < booster.Count; i++)
        {
            if (booster[i].bodyPart == item.bodyPart)
            {
                booster[i].panel.gameObject.SetActive(false);
                booster[i].select.gameObject.SetActive(true);
            }
        }
        item.select.gameObject.SetActive(false);
        item.panel.gameObject.SetActive(true);
        item.countText.text = "1";

        DataBaseManager.Instance.BoosterId = item.id;
        DataBaseManager.Instance.Value = item.value;
        DataBaseManager.Instance.Count = 1;
        CallAddBooster();
    }

    public void OnPlusClick(CheckItem item)
    {
        DataBaseManager.Instance.Count++;
        item.countText.text = DataBaseManager.Instance.Count.ToString();
        CallAddBooster();
    }
    public void OnMinusClick(CheckItem item)
    {
        if (DataBaseManager.Instance.Count >= 1)
        {
            DataBaseManager.Instance.Count--;
            item.countText.text = DataBaseManager.Instance.Count.ToString();
            CallDeleteBooster();
        }
        if(DataBaseManager.Instance.Count == 0)
        {
            item.select.gameObject.SetActive(true);
            item.panel.gameObject.SetActive(false);
        }
    }

    void ListenOnButton(int index)
    {
        booster[index].select.onClick.AddListener(() => OnSelectClick(booster[index]));
    }

    void ListenOnPlusButton(int index)
    {
        booster[index].plus.onClick.AddListener(() => OnPlusClick(booster[index]));
    }
    void ListenOnMinusButton(int index)
    {
        booster[index].minus.onClick.AddListener(() => OnMinusClick(booster[index]));
    }

    void CallAddBooster()
    {
        StartCoroutine(AddBooster());
    }
    void CallDeleteBooster()
    {
        StartCoroutine(DeleteBooster());
    }
    void CallAllBoostersDelete()
    {
        StartCoroutine(DeleteAllBoosters());
    }
    IEnumerator AddBooster()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("booster_id", DataBaseManager.Instance.BoosterId);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/addBooster.php", webForm))
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
    IEnumerator DeleteBooster()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("booster_id", DataBaseManager.Instance.BoosterId);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/deleteBooster.php", webForm))
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

    IEnumerator DeleteAllBoosters()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("booster_id", DataBaseManager.Instance.BoosterId);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/deleteAllBoosters.php", webForm))
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
