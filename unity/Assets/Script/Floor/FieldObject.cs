using UnityEngine;

public class FieldObject : MonoBehaviour {
    public RitualItem item;

    public int HealAmount;
    public float DelayTime;

    public string EmptyTextShow = "Desk Is Empty";

    private void Start()
    {
        FieldObjectManager.Instance.Add(this);
        if (item != null)
        {
            item.Init(this);
        }
    }

    public virtual void Action(Player player)
    {
        if(item == null && HealAmount == 0)
        {
            GameObject.FindWithTag("UI").GetComponent<PlayerUI>().SetErrorMessage(EmptyTextShow, 2);
        }

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