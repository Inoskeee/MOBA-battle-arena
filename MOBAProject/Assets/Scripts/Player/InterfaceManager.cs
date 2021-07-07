using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button pauseButton;

    public float timerStart;
    public Text timerText;

    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        timerText.text = "5";
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if(isPaused == true)
        {
            SetPause();
        }
        else
        {
            ResetPause();
        }


        if(timerText.gameObject.activeSelf == true)
        {
            timerStart -= Time.deltaTime;
            timerText.text = Mathf.Round(timerStart).ToString();
            if (timerStart <= 0)
            {
                ResetTimer();
            }
        }
    }

    public void SetTimer()
    {
        timerStart = 5;
        timerText.gameObject.SetActive(true);
    }

    public void ResetTimer()
    {
        timerText.gameObject.SetActive(false);
        //pauseMenu.SetActive(false);
    }

    private void SetPause()
    {
        //pauseMenu.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    private void ResetPause()
    {
        pauseButton.gameObject.SetActive(false);
        //pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
}
