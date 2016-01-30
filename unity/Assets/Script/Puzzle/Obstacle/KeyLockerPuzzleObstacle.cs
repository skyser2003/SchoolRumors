class KeyLockerPuzzleObstacle : PuzzleObstacle {
    public KeyHandheldItem item;

    public override void Action(Player player)
    {
        if (item == null) {
            return;
        }

        if (player.HandheldItem == null || player.HandheldItem.GetType() != typeof(WrenchHandheldItem)) {
            return;
        }

        item.GetPickedUp(player);
        item = null;
    }
}