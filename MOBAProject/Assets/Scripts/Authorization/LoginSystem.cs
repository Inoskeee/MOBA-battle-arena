using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSystem : MonoBehaviour
{
    public InputField nameUser;
    public InputField passwordUser;
    public Text errorText;


    public Button confirmButton;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    IEnumerator Login()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("name", nameUser.text);
        webForm.AddField("password", passwordUser.text);

        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/authorization.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //webRequest.SetRequestHeader("User-Agent", "runscope/0.1");
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                errorText.text = webRequest.error;
                errorText.enabled = true;
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                if (webRequest.downloadHandler.text.Split('\t')[0].ToString() != "0")
                {
                    errorText.color = Color.red;
                    errorText.text = RegistrationSystem.ErrorText(webRequest.downloadHandler.text);
                    errorText.gameObject.SetActive(true);
                }
                else
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    DataBaseManager.Instance.Name = nameUser.text;
                    DataBaseManager.Instance.Email = getChecker[1];
                    DataBaseManager.Instance.Score = int.Parse(getChecker[2]);
                    DataBaseManager.Instance.HeadId = int.Parse(getChecker[3]);
                    DataBaseManager.Instance.BodyId = int.Parse(getChecker[4]);
                    DataBaseManager.Instance.LegId = int.Parse(getChecker[5]);
                    DataBaseManager.Instance.SwordId = int.Parse(getChecker[6]);
                    DataBaseManager.Instance.BowId = int.Parse(getChecker[7]);
                    DataBaseManager.Instance.clanName = getChecker[8];
                    DataBaseManager.Instance.clanOwner = getChecker[9];
                    if (int.Parse(getChecker[10]) != 0)
                    {
                        DataBaseManager.Instance.BoosterId = int.Parse(getChecker[10]);
                        DataBaseManager.Instance.Value = int.Parse(getChecker[11]);
                        DataBaseManager.Instance.Count = int.Parse(getChecker[12]);
                    }
                    SceneManager.LoadScene(1);
                }
            }

        }
    }
}
