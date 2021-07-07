using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceForPlayer : MonoBehaviour
{
    public Text raiting;
    public GameObject booster;
    public Text healthBoost;
    public Text boosterCount;
    public Text arrows;

    //private PhotonView photonView;
    void Start()
    {
        //photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        /*if (!photonView.isMine)
        {
            booster.SetActive(false);
            raiting.gameObject.SetActive(false);
            return;
        }*/

        raiting.text = "Счет: " + DataBaseManager.Instance.Score;

        if(DataBaseManager.Instance.Count <= 0)
        {
            booster.SetActive(false);
        }

        healthBoost.text = "+" + DataBaseManager.Instance.Value + "HP";
        boosterCount.text = "X" + DataBaseManager.Instance.Count;
    }
}
