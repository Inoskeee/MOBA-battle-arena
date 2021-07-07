using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager Instance { get; set; }

    [Header("Person")]
    public string Name;
    public string Email;
    public string clanName;
    public string clanOwner;

    public int Armor;
    public int Score;

    public int SwordDamage;
    public int BowDamage;
    public int Arrows;

    [Header("Customization")]
    public int HeadId;
    public int BodyId;
    public int LegId;

    [Header("Weapon")]
    public int SwordId;
    public int BowId;

    [Header("Boosters")]
    public int BoosterId;
    public int Value;
    public int Count;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
