using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public GameObject registration;
    public GameObject login;

    private void Start()
    {
        registration = transform.Find("Registration").gameObject;
        login = transform.Find("Authorization").gameObject;
    }

    public void GoToRegister()
    {
        login.SetActive(false);
        registration.SetActive(true);
    }

    public void GoBackFromRegister()
    {
        registration.SetActive(false);
        login.SetActive(true);
    }



}
