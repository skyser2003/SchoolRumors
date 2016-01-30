﻿using UnityEngine;

class KeyLockerPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;

    public KeyHandheldItem item;
    public string ErrorText;

    protected override void Start()
    {
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
    }

    public override void Action(Player player)
    {
        if (item == null) {
            return;
        }

        if (player.HandheldItem == null || player.HandheldItem.GetType() != typeof(WrenchHandheldItem)) {
            playerUI.SetErrorMessage(ErrorText, 2);
            return;
        }

        item.GetPickedUp(player);
        item = null;
    }
}