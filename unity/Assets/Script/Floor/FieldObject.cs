using UnityEngine;

class FieldObject : MonoBehaviour {
    public RitualItem item;

    public int HealAmount;
    
    private void Start()
    {
        FieldObjectManager.Instance.Add(this);
    }

    public void Action(Player player)
    {
        if (item != null) {
            player.AcquireItem(item);
            item = null;
        }

        if (HealAmount != 0) {
            var playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
            playerUI.Heal(HealAmount);
        }
    }
}