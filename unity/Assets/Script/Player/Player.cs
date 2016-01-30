using UnityEngine;

class Player : MonoBehaviour {
    private RitualItemInventory inventory;

    public RitualItemInventory Inventory { get { return inventory; } }

    private void Start()
    {
        inventory = GetComponent<RitualItemInventory>();
    }

    public void AcquireItem(RitualItem item)
    {
        inventory.AddItem(item);
    }
}