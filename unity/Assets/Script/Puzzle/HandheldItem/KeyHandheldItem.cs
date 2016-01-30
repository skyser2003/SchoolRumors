using UnityEngine;

class KeyHandheldItem : HandheldItem {
    public override void Action(PuzzleObstacle obstacle)
    {
        if (obstacle == null) {
            return;
        }

        if (obstacle.GetType() == typeof(DoorPuzzleObstacle)) {
            obstacle.Action(player);
        }
    }
}