using UnityEngine;

class LockerPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;

    public RitualItem item;
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

        player.AcquireItem(item);
        item = null;
    }
}