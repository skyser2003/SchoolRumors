using UnityEngine;

public class Player : MonoBehaviour {
    private PlayerUI playerUI;

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

            var arm = transform.Find("Hand");

            // Temporarily set to right direction
            float direction = arm.transform.localScale.x;
            arm.transform.localScale = new Vector3(1, 1, 1);

            handheldItem.transform.parent = arm;
            handheldItem.transform.localPosition = new Vector3(0, 0, 0);
            handheldItem.transform.localEulerAngles = new Vector3(0, 0, 0);
            HandheldItemManager.Instance.Remove(handheldItem);

            playerUI.handheldTooltip.text = handheldItem.tooltip;

            // Recover original direction
            arm.transform.localScale = new Vector3(direction, 1, 1);

            if (prevItem != null) {
                prevItem.transform.parent = null;
                HandheldItemManager.Instance.Add(prevItem);

                var prevScale = prevItem.transform.localScale;
                prevScale.x = Mathf.Abs(prevScale.x);

                prevItem.transform.localScale = prevScale;
                prevItem.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
        }
    }

    private void Start()
    {
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
        inventory = GetComponent<RitualItemInventory>();
    }

    public void AcquireItem(RitualItem item)
    {
        inventory.AddItem(item);
    }
}