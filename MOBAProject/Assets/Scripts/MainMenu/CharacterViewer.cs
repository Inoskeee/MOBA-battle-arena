using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class CharacterViewer : MonoBehaviour
{
    private ItemList equips;


    void Start()
    {
        equips = GetComponent<ItemList>();
        equips.clothes[DataBaseManager.Instance.HeadId - 1].SetActive(true);
        equips.clothes[DataBaseManager.Instance.BodyId- 1].SetActive(true);
        equips.clothes[DataBaseManager.Instance.LegId - 1].SetActive(true);
        equips.swords[DataBaseManager.Instance.SwordId - 1].SetActive(false);
        equips.bows[DataBaseManager.Instance.BowId - 1].SetActive(false);
        equips.arrow.SetActive(false);

        if(DataBaseManager.Instance.Armor <= 0)
        {
            OnArmor();
            OnSword();
            OnBow();
        }
    }

    void Update()
    {
        
    }

    public void OnArmor()
    {
        StartCoroutine(GetArmorSelects(DataBaseManager.Instance.HeadId));
        StartCoroutine(GetArmorSelects(DataBaseManager.Instance.BodyId));
        StartCoroutine(GetArmorSelects(DataBaseManager.Instance.LegId));
    }

    public void OnSword()
    {
        StartCoroutine(GetSwordSelects());
    }

    public void OnBow()
    {
        StartCoroutine(GetBowSelects());
    }
    IEnumerator GetArmorSelects(int id)
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("item_id", id);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/armorGet.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    DataBaseManager.Instance.Armor += int.Parse(getChecker[2]);
                }
            }

        }
    }

    IEnumerator GetSwordSelects()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("item_id", DataBaseManager.Instance.SwordId);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/swordGet.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    DataBaseManager.Instance.SwordDamage = int.Parse(getChecker[2]);
                }
            }

        }
    }

    IEnumerator GetBowSelects()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("item_id", DataBaseManager.Instance.BowId);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/bowGet.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    DataBaseManager.Instance.BowDamage = int.Parse(getChecker[2]);
                    DataBaseManager.Instance.Arrows = int.Parse(getChecker[3]);

                }
            }

        }
    }


}
