﻿using UnityEngine;

class DoorPuzzleObstacle : PuzzleObstacle {
    public override void Action(Player player)
    {
        transform.Rotate(0, 90, 0);
    }
}