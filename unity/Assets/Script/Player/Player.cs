using UnityEngine;

class Player : MonoBehaviour {
    private RitualItemInventory inventory;
    private HandheldItem handheldItem;

    public RitualItemInventory Inventory { get { return inventory; } }
    public HandheldItem HandheldItem
    {
        get
        {
            return handheldItem;
        }
        set
        {
            handheldItem = value;
            var arm = transform.Find("Graphics/Quad");
            handheldItem.transform.parent = arm;
        }
    }

    private void Start()
    {
        inventory = GetComponent<RitualItemInventory>();
    }

    public void AcquireItem(RitualItem item)
    {
        inventory.AddItem(item);
    }
}