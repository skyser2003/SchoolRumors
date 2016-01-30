using UnityEngine;

class KeyHandheldItem : HandheldItem {
    public override void Action()
    {
        var obstacle = PuzzleObstacleManager.Instance.FindClosest(transform.position, 1);
        if (obstacle != null) {
            obstacle.Action(player);
        }
    }
}