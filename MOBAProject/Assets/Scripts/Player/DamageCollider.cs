using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DamageCollider : MonoBehaviour
{
    public ManageCharacter weaponDamage;
    public int currentWeaponDamage;

    private Collider damageCollider;

    public PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (gameObject.tag == "Arrow")
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = true;
        }
        else
        {
            damageCollider = GetComponent<Collider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }
    }


    private void Update()
    {
        //Перенести в другой скрипт
        Ray ray = new Ray(transform.position, transform.right);

        Debug.DrawRay(transform.position, ray.direction * 20f, Color.green);

        if (gameObject.tag == "Arrow")
        {
            transform.Translate(Vector3.right * 20f * Time.deltaTime);
            if(Vector3.Distance(Vector3.zero, gameObject.transform.position) >= 50)
            {
                Destroy(gameObject);
            }
        }
    }

    public void EnableDamageCollider()
    {
        damageCollider.enabled = true;
    }

    public void DisableDamageCollider()
    {
        damageCollider.enabled = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ManageCharacter playerStats = other.GetComponent<ManageCharacter>();
            playerStats.TakeDamage(currentWeaponDamage);
            if(playerStats.currentHealth - currentWeaponDamage <= 0)
            {
                if (photonView.isMine)
                {
                    DataBaseManager.Instance.Score += 20;
                    CallUpdateScore();
                }
            }
        }

        if (gameObject.tag == "Arrow")
        {
            Destroy(gameObject);
        }
    }


    void CallUpdateScore()
    {
        StartCoroutine(UpdatePlayerScore());
    }
    IEnumerator UpdatePlayerScore()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("score", DataBaseManager.Instance.Score);
        webForm.AddField("clanName", DataBaseManager.Instance.clanName);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/updatePlayerScore.php", webForm))
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
