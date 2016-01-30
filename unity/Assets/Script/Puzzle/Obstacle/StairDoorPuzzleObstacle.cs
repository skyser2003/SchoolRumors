using UnityEngine;

class StairDoorPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;
    private bool isOpen;

    public string ConditionTextShow = "You need all ritual items to go to the next floor";
    public int FloorLevel;

    protected override void Start()
    {
        base.Start();
        PuzzleObstacleManager.Instance.Add(this);
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
        isOpen = false;
    }

    public override bool CheckIfActionIsPossible(Player player)
    {
        if (RitualItem.HaveAllRitualItems(player.Inventory, FloorLevel) == false) {
            playerUI.SetErrorMessage(ConditionTextShow, 2);
            return false;
        }
        else if (isOpen == true) {
            return false;
        }
        else {
            return true;
        }
    }

    public override void Action(Player player)
    {
        isOpen = true;
        transform.Rotate(0, 90, 0);
    }
}