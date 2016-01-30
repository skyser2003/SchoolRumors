using System;
using UnityEngine;

class KeyLockerPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;

    public KeyHandheldItem item;
    public string ErrorText;

    protected override void Start()
    {
        base.Start();
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
    }

    public override bool CheckIfActionIsPossible(Player player)
    {
        if (item == null) {
            return false;
        }

        if (player.HandheldItem == null || player.HandheldItem.GetType() != typeof(WrenchHandheldItem)) {
            playerUI.SetErrorMessage(ErrorText, 2);
            return false;
        }

        return true;
    }

    public override void Action(Player player)
    {
        item.GetPickedUp(player);
        item = null;
    }
}