using System.Collections.Generic;
using UnityEngine;

class RitualItemInventory : MonoBehaviour
{
    private List<RitualItem> itemList = new List<RitualItem>();

    public int Count { get { return itemList.Count; } }

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
}