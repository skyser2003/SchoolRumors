using UnityEngine;

abstract class HandheldItem : MonoBehaviour {
    protected Player player;
    public bool IsHeldByPlayer;

    private void Start()
    {
        IsHeldByPlayer = false;
    }

    public abstract void Action();

    public void GetPickedUp(Player player)
    {
        this.player = player;
        player.HandheldItem = this;
    }
}