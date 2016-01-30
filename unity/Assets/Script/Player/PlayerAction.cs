using UnityEngine;

class PlayerAction : MonoBehaviour {
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            var fieldObject = FieldObjectManager.Instance.FindClosest(transform.position, 1);
            var handheldItem = HandheldItemManager.Instance.FindClosest(transform.position, 1);
            var puzzleObstacle = PuzzleObstacleManager.Instance.FindClosest(transform.position, 1);

            if (fieldObject != null && fieldObject.item != null) {
                fieldObject.GiveItem(player);
            }
            else if (handheldItem != null) {
                handheldItem.GetPickedUp(player);
                HandheldItemManager.Instance.Remove(handheldItem);
            }
            else if (puzzleObstacle != null && player.HandheldItem != null) {
                player.HandheldItem.Action();
            }
        }
    }
}