using UnityEngine;

class DoorPuzzleObstacle : PuzzleObstacle {
    private bool isOpen;

    public bool isLockRequired;
    public KeyHandheldItem keyItem;

    private PlayerUI playerUI;

    public string LockedTextShow = "Door is locked";

    protected override void Start()
    {
        base.Start();
        isOpen = false;
        PuzzleObstacleManager.Instance.Add(this);
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
    }

    public override bool CheckIfActionIsPossible(Player player)
    {
        if (isLockRequired == true && player.HandheldItem != keyItem) {
            playerUI.SetErrorMessage(LockedTextShow, 2);
            return false;
        }
        else {
            return true;
        }
    }

    public override void Action(Player player)
    {
        isLockRequired = false;
        isOpen = !isOpen;
        DelayTime = 0;

        if (isOpen == true) {
            transform.Rotate(0, 90, 0);
        }
        else {
            transform.Rotate(0, -90, 0);
        }
    }
}