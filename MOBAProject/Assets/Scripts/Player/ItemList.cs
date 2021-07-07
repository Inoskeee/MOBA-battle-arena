using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    public List<GameObject> clothes;
    public List<GameObject> swords;
    public List<GameObject> bows;

    public GameObject arrow;
    private void Awake()
    {
        for (int i = 0; i < clothes.Count; i++)
        {
            clothes[i].SetActive(false);
        }
        for (int i = 0; i < swords.Count; i++)
        {
            swords[i].SetActive(false);
        }
        for (int i = 0; i < bows.Count; i++)
        {
            bows[i].SetActive(false);
        }
    }

    void Update()
    {
        
    }
}
