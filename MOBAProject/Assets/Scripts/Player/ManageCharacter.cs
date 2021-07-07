using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UnityEngine.UI;

public class ManageCharacter : MonoBehaviour, IPunObservable
{

    public GameObject model;
    public GameManager gameManager;
    public InterfaceManager interfaceManager;

    [Header("Equipments")]
    public ItemList equips;
    public int head;
    public int body;
    public int leg;
    public int sword;
    public int bow;
    public int swordDamage;
    public int bowDamage;


    [Header("Characteristics")]
    public float currentHealth;
    public float maxHealth;

    [Header("Move")]
    public float speed;
    public float rotationSpeed;


    //Работа с UI элементами
    [Header("UI")]
    public Canvas myGUI;
    public GameObject healthInterface;
    public Transform healthPoint;


    //private
    private HealthBar healthbar;

    public GameObject showHP; 
    private Vector3 mover;
    private Animator animList;
    private Rigidbody rigid;

    private Vector3 screenPos;

    private PhotonView photonView;
    private void Awake()
    {
        //myGUI = GameObject.FindGameObjectWithTag("HpCanvas").GetComponent<Canvas>();
        photonView = GetComponent<PhotonView>();

        if (photonView.isMine)
        {
            head = DataBaseManager.Instance.HeadId;
            body = DataBaseManager.Instance.BodyId;
            leg = DataBaseManager.Instance.LegId;
            sword = DataBaseManager.Instance.SwordId;
            bow = DataBaseManager.Instance.BowId;
            swordDamage = DataBaseManager.Instance.SwordDamage;
            bowDamage = DataBaseManager.Instance.BowDamage;
        }
        else
        {
            head = DataBaseManager.Instance.HeadId;
            body = DataBaseManager.Instance.BodyId;
            leg = DataBaseManager.Instance.LegId;
            sword = DataBaseManager.Instance.SwordId;
            bow = DataBaseManager.Instance.BowId;
            swordDamage = DataBaseManager.Instance.SwordDamage;
            bowDamage = DataBaseManager.Instance.BowDamage;
        }
        model.SetActive(true);

        showHP = (GameObject)Instantiate(healthInterface);
        showHP.transform.SetParent(myGUI.transform, true);

        rigid = gameObject.GetComponent<Rigidbody>();
        animList = model.GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar = showHP.GetComponent<HealthBar>();
        healthbar.health = currentHealth;
        healthbar.playerName.text = photonView.owner.NickName;
        gameManager = FindObjectOfType<GameManager>();
        interfaceManager = FindObjectOfType<InterfaceManager>();
    }

    private void Start()
    {
        if (photonView.isMine)
        {
            equips = model.GetComponent<ItemList>();
            for (int i = 0; i < equips.clothes.Count; i++)
            {
                equips.clothes[i].SetActive(false);
            }
            Equipments(head, body, leg);
            equips.swords[sword - 1].SetActive(true);
        }
    }

    private void Update()
    {

        if (!photonView.isMine)
        {
            HealthBar playerHealth = showHP.GetComponent<HealthBar>();
            playerHealth.health = currentHealth;
            playerHealth.hpinfo.text = currentHealth.ToString();
            if (showHP != null)
            {
                showHP.transform.position = screenPos;
            }
        }

        healthbar.health = currentHealth;
        healthbar.hpinfo.text = currentHealth.ToString();

        if (Input.GetKeyDown(KeyCode.Mouse0) && Cursor.visible == false && photonView.isMine)
        {
            animList.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.F) && Cursor.visible == false && photonView.isMine && DataBaseManager.Instance.Count > 0 && currentHealth<maxHealth)
        {
            if(currentHealth+DataBaseManager.Instance.Value >= maxHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += DataBaseManager.Instance.Value;
            }
            DataBaseManager.Instance.Count -= 1;
            CallDeleteBooster();
        }


        if(showHP != null)
        {
            Vector3 screenPos = healthPoint.position;
            showHP.transform.position = screenPos;
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.isMine)
        {
            Equipments(head, body, leg);
            return;
        }

        if (photonView.isMine)
        {
            Equipments(head, body, leg);
        }

        GetInput();
        //Задаем направление движения
        Vector3 movDirection = mover.normalized * speed;


        rigid.velocity = movDirection;


        animList.SetFloat("Speed", mover.x, 0.1f, Time.deltaTime);
        animList.SetFloat("Direction", mover.z, 0.1f, Time.deltaTime);

        //Поворот персонажа
        Rotation(movDirection);

    }
    private void GetInput()
    {
        mover.Set(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
        if (photonView.isMine)
        {
            photonView.RPC("RPC_TakeDamage", PhotonTargets.AllBuffered, damage);
            if (currentHealth <= 0)
            {
                photonView.RPC("RPC_Interface_Destroy", PhotonTargets.All);
                //photonView.RPC("RPC_HP_Destroy", PhotonTargets.All);
                interfaceManager.SetTimer();
                PhotonNetwork.Destroy(gameObject);
                gameManager.OnRespawn();
            }

        }      
    }

    [PunRPC]
    void RPC_TakeDamage(int damage)
    {
        if (!photonView.isMine)
        {
            return;
        }
        currentHealth -= damage;
    }


    [PunRPC]
    void RPC_HP_Destroy()
    {
        if (!photonView.isMine)
        {
            Destroy(showHP);
            return;
        }
        Destroy(showHP);
    }

    [PunRPC]
    void RPC_Interface_Destroy()
    {
        if (!photonView.isMine)
        {
            return;
        }
        Destroy(model.GetComponent<WeaponSlotManager>().playerInterface);
    }

    private void Rotation(Vector3 movDirection)
    {
        Vector3 targetDirection = movDirection;
        targetDirection.y = 0;
        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion pointTo = Quaternion.LookRotation(targetDirection);
        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, pointTo, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(screenPos);
            stream.SendNext(currentHealth);
            stream.SendNext(head);
            stream.SendNext(body);
            stream.SendNext(leg);
            stream.SendNext(bow);
            stream.SendNext(sword);
            stream.SendNext(swordDamage);
            stream.SendNext(bowDamage);
        }
        if (stream.isReading)
        {
            screenPos = (Vector3)stream.ReceiveNext();
            currentHealth = (float)stream.ReceiveNext();
            head = (int)stream.ReceiveNext();
            body = (int)stream.ReceiveNext();
            leg = (int)stream.ReceiveNext();
            bow = (int)stream.ReceiveNext();
            sword = (int)stream.ReceiveNext();
            swordDamage = (int)stream.ReceiveNext();
            bowDamage = (int)stream.ReceiveNext();
        }
    }

    void Equipments(int head, int body, int leg)
    {
        equips = model.GetComponent<ItemList>();

        equips.clothes[head - 1].SetActive(true);
        equips.clothes[body - 1].SetActive(true);
        equips.clothes[leg- 1].SetActive(true);
    }

    void CallDeleteBooster()
    {
        StartCoroutine(DeleteBooster());
    }

    IEnumerator DeleteBooster()
    {
        WWWForm webForm = new WWWForm();
        webForm.AddField("username", DataBaseManager.Instance.Name);
        webForm.AddField("booster_id", DataBaseManager.Instance.BoosterId);

        //Ссылка на PHP запросы
        using (UnityWebRequest webRequest = UnityWebRequest.Post("http://localhost/deleteBooster.php", webForm))
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
