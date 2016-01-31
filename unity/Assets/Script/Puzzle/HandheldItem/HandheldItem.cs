using UnityEngine;

abstract class HandheldItem : MonoBehaviour {
    protected Player player;
    public bool IsHeldByPlayer;
    public string tooltip;

    private void Start()
    {
        IsHeldByPlayer = false;
        HandheldItemManager.Instance.Add(this);
    }

    public void GetPickedUp(Player player)
    {
        this.player = player;
        player.HandheldItem = this;
    }
}