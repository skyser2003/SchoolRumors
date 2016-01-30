using UnityEngine;

class FieldObject : MonoBehaviour {
    public Item item;

    private void Start()
    {
        FieldObjectManager.Instance.Add(this);
    }

    public void GiveItem(Player player)
    {
        if (item == null) {
            return;
        }

        player.AcquireItem(item);
        item = null;
    }
}
