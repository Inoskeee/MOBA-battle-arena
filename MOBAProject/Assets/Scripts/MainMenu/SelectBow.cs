using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBow : MonoBehaviour
{
    public ItemList items;
    public List<CheckItem> bow;

    private void Start()
    {
        for (int i = 0; i < bow.Count; i++)
        {
            if (DataBaseManager.Instance.BowId - 1 == i)
            {
                bow[i].select.interactable = false;
            }
            bow[i].id = i + 1;
            bow[i].OnBow();
            bow[i].thisItem = items.bows[i];
            ListenOnButton(i);
        }
    }

    public void OnSelectClick(CheckItem item)
    {
        for (int i = 0; i < bow.Count; i++)
        {
            if (bow[i].bodyPart == item.bodyPart)
            {
                bow[i].select.interactable = true;
                bow[i].thisItem.SetActive(false);
            }
        }
        item.select.interactable = false;
        item.thisItem.SetActive(true);

        DataBaseManager.Instance.BowId = item.id;
        DataBaseManager.Instance.BowDamage = item.bowDamage;
        DataBaseManager.Instance.Arrows = item.arrows;
    }

    void ListenOnButton(int index)
    {
        bow[index].select.onClick.AddListener(() => OnSelectClick(bow[index]));
    }
}
