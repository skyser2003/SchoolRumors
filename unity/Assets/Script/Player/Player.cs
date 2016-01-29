using UnityEngine;

class Player : MonoBehaviour {
    private Inventory inventory;

    private int maxHP;
    private int currentHP;

    public Inventory Inventory { get { return inventory; } }

    private void Start()
    {
        inventory = GetComponent<Inventory>();
        currentHP = maxHP;
    }

    public void AcquireItem(Item item)
    {
        inventory.AddItem(item);
    }

    public void Damage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0) {
            GameProgressManager.Instance.GameOver();
        }
    }
}