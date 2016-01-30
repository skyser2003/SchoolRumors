using UnityEngine;

class Player : MonoBehaviour {
    private Inventory inventory;

    public Inventory Inventory { get { return inventory; } }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    public void AcquireItem(Item item)
    {
        inventory.AddItem(item);
    }
}