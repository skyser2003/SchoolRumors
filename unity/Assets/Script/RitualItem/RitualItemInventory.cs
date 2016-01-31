using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class RitualItemInventory : MonoBehaviour {
    private List<RitualItem> itemList = new List<RitualItem>();

    public ReadOnlyCollection<RitualItem> ItemList { get { return itemList.AsReadOnly(); } }

    PlayerUI playerUI;
    public AudioSource audioGetItem;

    private void Start()
    {
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
    }

    public void AddItem(RitualItem item)
    {
        audioGetItem.Play();
        playerUI.SetRitualItem(item.id, true);
        itemList.Add(item);
    }

    public int Count(int floorLevel)
    {
        int count = 0;

        foreach (var item in itemList) {
            if (item.FloorLevel == floorLevel) {
                ++count;
            }
        }

        return count;
    }
}