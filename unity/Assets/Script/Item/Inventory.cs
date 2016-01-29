using System.Collections.Generic;
using UnityEngine;

class Inventory : MonoBehaviour {
    private List<Item> itemList = new List<Item>();

    public int Count { get { return itemList.Count; } }

    private void Start()
    {

    }

    private void FixedUpdate()
    {

    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }
}