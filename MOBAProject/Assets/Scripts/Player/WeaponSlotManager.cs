using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WeaponSlotManager : Photon.PunBehaviour
{
    public ManageCharacter weapons;

    public GameObject sword;
    public GameObject bow;
    public GameObject arrow;

    public GameObject InterfacePrefab;
    public GameObject playerInterface;
    public Text arrowsText;

    public Animator animlist;

    public DamageCollider firstWeapon;
    public BowManager secondWeapon;

    private new PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();

    }

    private void Start()
    {
        if (photonView.isMine)
        {
            sword = GetComponent<ItemList>().swords[DataBaseManager.Instance.SwordId - 1];
            sword.SetActive(true);
            bow = GetComponent<ItemList>().bows[DataBaseManager.Instance.BowId - 1];
            firstWeapon = sword.GetComponent<DamageCollider>();
        }

        bow.SetActive(false);

        playerInterface = Instantiate(InterfacePrefab);
        playerInterface.transform.SetParent(gameObject.transform, true);

        Canvas interfacePlayerRender = playerInterface.GetComponent<Canvas>();

        interfacePlayerRender.renderMode = RenderMode.ScreenSpaceCamera;
        interfacePlayerRender.worldCamera = GameObject.FindObjectOfType<Camera>();
        interfacePlayerRender.planeDistance = 1;
        //Получаем стрелы
        arrowsText = playerInterface.GetComponent<InterfaceForPlayer>().arrows;

        arrowsText.gameObject.SetActive(false);
        arrowsText.text = $"Стрелы: {DataBaseManager.Instance.Arrows}";
        animlist = GetComponent<Animator>();
        SetSword();
    }
    private void Update()
    {
        if (!photonView.isMine)
        {
            playerInterface.SetActive(false);
            //arrowsText.gameObject.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(2);
        }
        arrowsText.text = $"Стрелы: {DataBaseManager.Instance.Arrows}";
    }

    private void EquipItem(int index)
    {
        if (index == 1)
        {
            SetSword();
            arrowsText.gameObject.SetActive(false);
        }
        else if (index == 2)
        {
            SetBow();
            arrowsText.gameObject.SetActive(true);
        }


        if (photonView.isMine)
        {
            Hashtable hashTable = new Hashtable();
            hashTable.Add("index", index);
            PhotonNetwork.player.SetCustomProperties(hashTable);
        }
    }

    public override void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps)
    {
        PhotonPlayer player = playerAndUpdatedProps[0] as PhotonPlayer;
        if (!photonView.isMine && player == photonView.owner)
        {
            Hashtable hashTable = playerAndUpdatedProps[1] as Hashtable;
            EquipItem((int)hashTable["index"]);
        }
    }
    private void SetBow()
    {
        sword.SetActive(false);
        animlist.SetBool("isBow", true);
        bow.SetActive(true);
        arrow.SetActive(true);
    }

    private void SetSword()
    {
        bow.SetActive(false);
        arrow.SetActive(false);
        animlist.SetBool("isBow", false);
        sword.SetActive(true);
    }

    public void OpenFirstDamageCollider()
    {
        firstWeapon.EnableDamageCollider();
    }
    public void CloseFirstDamageCollider()
    {
        firstWeapon.DisableDamageCollider();
    }
    public void CreationArrowCollider()
    {
        secondWeapon.CreationArrow();
    }

}
