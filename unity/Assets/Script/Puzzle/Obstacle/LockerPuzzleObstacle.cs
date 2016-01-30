class LockerPuzzleObstacle : PuzzleObstacle {
    public RitualItem item;

    public override void Action(Player player)
    {
        if (item == null) {
            return;
        }

        if (player.HandheldItem == null || player.HandheldItem.GetType() != typeof(WrenchHandheldItem)) {
            return;
        }

        player.AcquireItem(item);
        item = null;
    }
}