using System;
using UnityEngine;

class KeyLockerPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;

    public KeyHandheldItem item;
    public string ErrorText;
    private string IsEmptyText = "Locker is empty";

    public GameObject LockedGraphic;
    public GameObject UnlockedGraphic;

    protected override void Start()
    {
        base.Start();
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
        LockedGraphic.gameObject.SetActive(true);
        UnlockedGraphic.gameObject.SetActive(false);
    }

    public override bool CheckIfActionIsPossible(Player player)
    {
        //if (item == null) {
        //    return false;
        //}

        if (player.HandheldItem == null || player.HandheldItem.GetType() != typeof(WrenchHandheldItem)) {
            playerUI.SetErrorMessage(ErrorText, 2);
            return false;
        }

        return true;
    }

    public override void Action(Player player)
    {
        if(item == null)
        {
            playerUI.SetErrorMessage(IsEmptyText, 2);
        }
        if (item != null)
        {
            item.GetPickedUp(player);
        }
        item = null;
        UnlockedGraphic.gameObject.SetActive(true);
        LockedGraphic.gameObject.SetActive(false);
    }
}