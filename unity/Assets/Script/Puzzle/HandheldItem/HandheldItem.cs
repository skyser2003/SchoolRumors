using UnityEngine;

abstract class HandheldItem : MonoBehaviour {
    protected Player player;
    public bool IsHeldByPlayer;

    private void Start()
    {
        IsHeldByPlayer = false;
        HandheldItemManager.Instance.Add(this);
    }

    public abstract void Action(PuzzleObstacle obstacle);

    public void GetPickedUp(Player player)
    {
        this.player = player;
        player.HandheldItem = this;
    }
}