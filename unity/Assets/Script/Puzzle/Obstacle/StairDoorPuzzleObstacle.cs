using UnityEngine;

class StairDoorPuzzleObstacle : PuzzleObstacle {
    private PlayerUI playerUI;
    private bool isOpen;

    public string ConditionTextShow = "You need all ritual items to go to the next floor";

    protected override void Start()
    {
        base.Start();
        PuzzleObstacleManager.Instance.Add(this);
        playerUI = GameObject.FindWithTag("UI").GetComponent<PlayerUI>();
        isOpen = false;
    }

    public override void Action(Player player)
    {
        if (player.Inventory.Count != RitualItem.allItems.Count) {
            playerUI.SetErrorMessage(ConditionTextShow, 2);
            return;
        }
        else if (isOpen == false) {
            isOpen = true;
            transform.Rotate(0, 90, 0);
        }
    }
}