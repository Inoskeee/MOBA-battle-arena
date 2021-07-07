using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RegistrationSystem : MonoBehaviour
{
    public InputField nameUser;
    public InputField passwordUser;
    public InputField emailUser;
    public Text errorText;

    public Button confirmButton;

    public MainMenu menu;

    private void Start()
    {
        HideError();
    }
    public void CallRegister()
    {
        StartCoroutine(Registration());
    }

    IEnumerator Registration()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("name", nameUser.text);
        webForm.AddField("password", passwordUser.text);
        webForm.AddField("email", emailUser.text);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/register.php", webForm))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            //webRequest.SetRequestHeader("User-Agent", "runscope/0.1");
            yield return webRequest.SendWebRequest();
            Debug.Log(webRequest.downloadHandler.text);


            if (webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
                errorText.text = webRequest.error;
                errorText.enabled = true;
            }
            else
            {
                if (webRequest.downloadHandler.text == "0")
                {
                    errorText.color = Color.green;
                    errorText.text = "Успешная регистрация!";
                    errorText.gameObject.SetActive(true);
                    menu.GoBackFromRegister();
                }
                else
                {
                    errorText.color = Color.red;
                    errorText.text = ErrorText(webRequest.downloadHandler.text);
                    errorText.gameObject.SetActive(true);
                }
            }
        }

    }

    public void VerifyInputs()
    {
        confirmButton.interactable = (nameUser.text.Length >= 8 && passwordUser.text.Length >= 8 && emailUser.text.Length >= 8);
    }

    public void HideError()
    {
        errorText.gameObject.SetActive(false);
    }

    //Коды ошибок
    public static string ErrorText(string errorCode)
    {
        switch (errorCode)
        {
            case "1":
                return "Ошибка соединения с сервером. Повторите попытку.";
            case "2":
                return "Ошибка запроса на сервер. Повторите попытку.";
            case "3":
                return "Это имя уже занято. Повторите попытку.";
            case "4":
                return "Email должен быть в формате: abcd123@tttt.tt";
            case "5":
                return "Ошибка в сохранении данных игрока. Повторите попытку.";
            case "6":
                return "Не найдено ни одного игрока с таким ником. Проверьте правильность ввода.";
            case "7":
                return "Введенный пароль является некорректным. Повторите попытку.";
            case "8":
                return "Этот игрок уже состоит в клане.";
        }
        return "";
    }
}
