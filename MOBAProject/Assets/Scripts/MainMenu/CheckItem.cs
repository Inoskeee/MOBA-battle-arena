using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class CheckItem : MonoBehaviour
{
    public int id;
    
    public string bodyPart;

    public int armor;
    public int swordDamage;
    public int bowDamage;
    public int arrows;
    public int value;

    public GameObject thisItem;
    public Button select;
    public Text itemName;
    public Text info;

    [Header("Counts")]
    public GameObject panel;
    public Text countText;
    public Button plus;
    public Button minus;

    private void Start()
    {
    }

    public void OnArmor()
    {
        StartCoroutine(GetArmorSelects());
    }

    public void OnSword()
    {
        StartCoroutine(GetSwordSelects());
    }

    public void OnBow()
    {
        StartCoroutine(GetBowSelects());
    }

    public void OnBoost()
    {
        StartCoroutine(GetBoostersSelects());
    }
    IEnumerator GetArmorSelects()
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
                    itemName.text = getChecker[1];
                    info.text = $"Броня: {getChecker[2]}";
                    bodyPart = getChecker[3];
                    armor = int.Parse(getChecker[2]);
                }
            }

        }
    }

    IEnumerator GetSwordSelects()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("item_id", id);
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
                    itemName.text = getChecker[1];
                    info.text = $"Урон: {getChecker[2]}";
                    bodyPart = "hands";
                    swordDamage = int.Parse(getChecker[2]);
                }
            }

        }
    }

    IEnumerator GetBowSelects()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("item_id", id);
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
                    itemName.text = getChecker[1];
                    info.text = $"Урон: {getChecker[2]}\nКоличество стрел: {getChecker[3]}";
                    bodyPart = "hands";
                    bowDamage = int.Parse(getChecker[2]);
                    arrows = int.Parse(getChecker[3]);

                }
            }

        }
    }

    IEnumerator GetBoostersSelects()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("booster_id", id);
        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/boosterGet.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();

            if (!webRequest.isNetworkError)
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() == "0")
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    itemName.text = getChecker[1];
                    info.text = $"Восстановление здоровья: {getChecker[2]}";
                    bodyPart = "pocket";
                    value = int.Parse(getChecker[2]);
                }
            }

        }
    }
}
