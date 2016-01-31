using UnityEngine;
using System.Collections;

public class FieldObject_Ezel : FieldObject {

    public GameObject GainItemObject;
    public float PlayerSpeedChange = 1f;
    public override void Action(Player player)
    {
        if (item == null && HealAmount == 0)
        {
            GameObject.FindWithTag("UI").GetComponent<PlayerUI>().SetErrorMessage(EmptyTextShow, 2);
        }

        if (item != null)
        {
            GainItemObject.gameObject.SetActive(false);
            player.AcquireItem(item);
            player.gameObject.GetComponent<PlayerMovement>().Speed = PlayerSpeedChange;
            item = null;
        }

        if (HealAmount != 0)
        {
            var playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
            playerUI.Heal(HealAmount);
        }
    }
}
