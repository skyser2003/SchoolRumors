using UnityEngine;

class DoorPuzzleObstacle : PuzzleObstacle {
    private bool isOpen;

    public bool isLockRequired;
    public KeyHandheldItem keyItem;

    private PlayerUI playerUI;

    public string LockedTextShow = "Door is locked";
    public float initangle = 0f;

    protected override void Start()
    {
        base.Start();
        isOpen = false;
        PuzzleObstacleManager.Instance.Add(this);
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
        initangle = transform.localEulerAngles.y;
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
            //transform.Rotate(0, 90, 0);
            transform.localEulerAngles = new Vector3(0f, initangle + 90f, 0f);
        }
        else {

            transform.localEulerAngles = new Vector3(0f, initangle, 0f);
        }
    }

    public void Close()
    {
        isOpen = false;
        transform.localEulerAngles = new Vector3(0f, initangle, 0f);
    }
}