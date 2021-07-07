using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class SendMail : MonoBehaviour
{
    public InputField playerName;
    public Text errorText;

    public string checker;

    public string clanRaiting;
    public string clanLeader;
    public string clanName;
    public string playerEmail;

    public List<string> clanMembers;
    private void Start()
    {
        checker = "0";
        clanMembers = new List<string>();
        errorText.gameObject.SetActive(false);
        CallPlayers();
    }

    public void OnSendMail()
    {
        CallThisPlayer();
    }

    void SendEmail()
    {
        MailMessage message = new MailMessage();
        message.Body = $"Здравствуйте {playerName.text}! Вы получили приглашение в клан {clanName}. Ознакомьтесь с информацией о клане и примите решение:";
        message.Body += $"\nЛидер клана: {clanLeader}.\nРейтинг клана: {clanRaiting}.\nУчастники клана:";
        for(int i = 0; i < clanMembers.Count; i++)
        {
            message.Body += $"\n{clanMembers[i]}";
        }
        message.From = new MailAddress("nvpopov.me@gmail.com");
        message.To.Add(playerEmail);
        message.BodyEncoding = System.Text.Encoding.UTF8;

        SmtpClient client = new SmtpClient();
        client.Host = "smtp.gmail.com";
        client.Port = 587;
        client.Credentials = new NetworkCredential(message.From.Address, "bmpkfpzbgfjymrxt");
        client.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
        client.Send(message);
    }

    public void CallPlayers()
    {
        StartCoroutine(GetPlayerClan());
    }
    public void CallThisPlayer()
    {
        StartCoroutine(GetThisPlayer());
    }
    IEnumerator GetThisPlayer()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("name", playerName.text);

        //Ссылка
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/getcurrentplayer.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
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
                    checker = "1";
                }
                else
                {
                    string[] getChecker = webRequest.downloadHandler.text.Split('\t');
                    checker = getChecker[0];
                    playerEmail = getChecker[1];
                    SendEmail();
                    errorText.gameObject.SetActive(true);
                    errorText.color = Color.green;
                    errorText.text = "сообщение отправлено!";
                }
            }

        }
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
                    clanName = getChecker[1];
                    clanRaiting = getChecker[2];
                    clanLeader = getChecker[3];
                    for (int i = 4; i < getChecker.Length - 1; i++)
                    {
                        clanMembers.Add(getChecker[i]);
                    }
                }
            }

        }
    }
}
