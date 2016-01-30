using UnityEngine;

abstract class PuzzleObstacle : MonoBehaviour {
    public abstract void Action(Player player);
}