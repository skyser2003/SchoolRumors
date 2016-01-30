using UnityEngine;

class DoorPuzzleObstacle : PuzzleObstacle {
    private bool isOpen;

    public bool isLockRequired;
    public KeyHandheldItem keyItem;

    protected override void Start()
    {
        base.Start();
        isOpen = false;
        PuzzleObstacleManager.Instance.Add(this);
    }

    public override void Action(Player player)
    {
        if (isLockRequired == true && player.HandheldItem != keyItem) {
            return;
        }

        isLockRequired = false;
        isOpen = !isOpen;

        if (isOpen == true) {
            transform.Rotate(0, 90, 0);
        }
        else {
            transform.Rotate(0, -90, 0);
        }
    }
}