using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SelectArmor : MonoBehaviour
{
    public ItemList items;
    public List<CheckItem> armor;

    private void Awake()
    {
        for(int i = 0; i < armor.Count; i++)
        {
            if (DataBaseManager.Instance.HeadId - 1==i || DataBaseManager.Instance.BodyId - 1 == i || DataBaseManager.Instance.LegId - 1 == i)
            {
                armor[i].select.interactable = false;
            }
            armor[i].id = i + 1;
            armor[i].OnArmor();
            armor[i].thisItem = items.clothes[i];
            ListenOnButton(i);
        }

    }

    public void OnSelectClick(CheckItem item)
    {
        for(int i = 0; i < armor.Count; i++)
        {
            if (armor[i].bodyPart == item.bodyPart && armor[i].select.interactable == false)
            {
                armor[i].select.interactable = true;
                armor[i].thisItem.SetActive(false);
                DataBaseManager.Instance.Armor-=armor[i].armor;
            }
        }
        item.select.interactable = false;
        item.thisItem.SetActive(true);
        DataBaseManager.Instance.Armor += item.armor;

        if (item.bodyPart == "head")
        {
            DataBaseManager.Instance.HeadId = item.id;
        }
        else if (item.bodyPart == "body")
        {
            DataBaseManager.Instance.BodyId = item.id;
        }
        else if (item.bodyPart == "leg")
        {
            DataBaseManager.Instance.LegId = item.id;
        }
    }

    void ListenOnButton(int index)
    {
        armor[index].select.onClick.AddListener(() => OnSelectClick(armor[index]));
    }
}
