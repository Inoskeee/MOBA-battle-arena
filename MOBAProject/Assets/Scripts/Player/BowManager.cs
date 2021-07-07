using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowManager : MonoBehaviour
{
    public ManageCharacter character;
    public GameObject arrow;
    public PhotonView photonView;
    public void CreationArrow()
    {
        arrow.transform.position = gameObject.transform.position;
        arrow.transform.rotation = gameObject.transform.rotation;
        if (photonView.isMine)
        {
            DataBaseManager.Instance.Arrows--;
        }
        Instantiate(arrow).GetComponent<DamageCollider>().weaponDamage = character;
    }

}
