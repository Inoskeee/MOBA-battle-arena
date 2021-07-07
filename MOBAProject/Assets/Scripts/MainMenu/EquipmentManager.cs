using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    public List<Button> generalButtons;
    public List<GameObject> contents;

    public GameObject player;
    private ItemList items;
    private Animator anims;

    public ScrollRect scroller;

    public Text dataText;
    void Start()
    {
        dataText.text = $"Броня: {DataBaseManager.Instance.Armor}%";
        items = player.GetComponent<ItemList>();
        anims = player.GetComponent<Animator>();
    }

    void Update()
    {
        dataText.text = $"Броня: {DataBaseManager.Instance.Armor}%";
    }

    void SelectedButtons(int part)
    {
        for (int i = 0; i < generalButtons.Count; i++)
        {
            if (i == part)
            {
                generalButtons[i].interactable = false;
                contents[i].SetActive(true);
                scroller.content = contents[i].GetComponent<RectTransform>();
            }
            else
            {
                generalButtons[i].interactable = true;
                contents[i].SetActive(false);
            }
        }
    }
    public void OnArmorClick(int part)
    {
        SelectedButtons(part);
        anims.SetBool("hasSword", false);
        anims.SetBool("hasBow", false);
        for(int i = 0; i < items.swords.Count; i++)
        {
            items.swords[i].SetActive(false);
        }
        for (int i = 0; i < items.bows.Count; i++)
        {
            items.bows[i].SetActive(false);
        }
        items.arrow.SetActive(false);
    }
    public void OnSwordClick(int part)
    {
        SelectedButtons(part);
        anims.SetBool("hasSword", true);
        anims.SetBool("hasBow", false);
        for (int i = 0; i < items.swords.Count; i++)
        {
            if (DataBaseManager.Instance.SwordId - 1 == i)
            {
                items.swords[i].SetActive(true);
            }
            else
            {
                items.swords[i].SetActive(false);
            }
        }
        for (int i = 0; i < items.bows.Count; i++)
        {
            items.bows[i].SetActive(false);
        }
        items.arrow.SetActive(false);

    }
    public void OnBowClick(int part)
    {
        SelectedButtons(part);
        anims.SetBool("hasSword", false);
        anims.SetBool("hasBow", true);
        for (int i = 0; i < items.swords.Count; i++)
        {
            items.swords[i].SetActive(false);
        }
        for (int i = 0; i < items.bows.Count; i++)
        {
            if (DataBaseManager.Instance.BowId - 1 == i)
            {
                items.bows[i].SetActive(true);
            }
            else
            {
                items.bows[i].SetActive(false);
            }
        }
        items.arrow.SetActive(true);

    }


}
