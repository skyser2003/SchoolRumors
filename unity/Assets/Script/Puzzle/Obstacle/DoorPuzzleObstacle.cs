using UnityEngine;

class DoorPuzzleObstacle : PuzzleObstacle {
    private bool isOpen;

    public KeyHandheldItem keyItem;

    protected override void Start()
    {
        base.Start();
        isOpen = false;
        PuzzleObstacleManager.Instance.Add(this);
    }

    public override void Action(Player player)
    {
        if (isOpen == true || player.HandheldItem != keyItem) {
            return;
        }

        isOpen = true;
        transform.Rotate(0, 90, 0);
    }
}