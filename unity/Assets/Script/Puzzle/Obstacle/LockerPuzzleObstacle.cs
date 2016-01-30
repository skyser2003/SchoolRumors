class LockerPuzzleObstacle : PuzzleObstacle {
    public RitualItem item;

    public override void Action(Player player)
    {
        if (item == null) {
            return;
        }

        player.AcquireItem(item);
        item = null;
    }
}