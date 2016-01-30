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
            var prevItem = handheldItem;

            handheldItem = value;
            var arm = transform.Find("Graphics/Quad");
            handheldItem.transform.parent = arm;
            HandheldItemManager.Instance.Remove(handheldItem);

            if (prevItem != null) {
                prevItem.transform.parent = null;
                HandheldItemManager.Instance.Add(prevItem);
            }
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