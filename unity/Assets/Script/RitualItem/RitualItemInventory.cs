using System.Collections.Generic;
using UnityEngine;

class RitualItemInventory : MonoBehaviour {
    private List<RitualItem> itemList = new List<RitualItem>();

    public int Count { get { return itemList.Count; } }

    private void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    public void AddItem(RitualItem item)
    {
        itemList.Add(item);
    }
}