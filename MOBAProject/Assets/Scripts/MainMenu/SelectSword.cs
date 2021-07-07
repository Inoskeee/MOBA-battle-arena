using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSword : MonoBehaviour
{
    public ItemList items;
    public List<CheckItem> sword;

    private void Start()
    {
        for (int i = 0; i < sword.Count; i++)
        {
            if (DataBaseManager.Instance.SwordId - 1 == i)
            {
                sword[i].select.interactable = false;
            }
            sword[i].id = i + 1;
            sword[i].OnSword();
            sword[i].thisItem = items.swords[i];
            ListenOnButton(i);
        }
    }

    public void OnSelectClick(CheckItem item)
    {
        for (int i = 0; i < sword.Count; i++)
        {
            if (sword[i].bodyPart == item.bodyPart)
            {
                sword[i].select.interactable = true;
                sword[i].thisItem.SetActive(false);
            }
        }
        item.select.interactable = false;
        item.thisItem.SetActive(true);

        DataBaseManager.Instance.SwordId = item.id;
        DataBaseManager.Instance.SwordDamage = item.swordDamage;
    }
    void ListenOnButton(int index)
    {
        sword[index].select.onClick.AddListener(() => OnSelectClick(sword[index]));
    }
}
