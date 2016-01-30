using UnityEngine;

class PlayerAction : MonoBehaviour {
    private Player player;
    private PlayerMovement playerMove;

    private bool isSearching;
    private float curSearchTime;
    private FieldObject searchingObject;
    public float SearchDelayTime;

    private void Start()
    {
        player = GetComponent<Player>();
        playerMove = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        var dt = Time.deltaTime;

        if (isSearching == true) {
            // Keep searching
            if (playerMove.Direction.magnitude == 0) {
                curSearchTime -= dt;
                if (curSearchTime <= 0) {
                    searchingObject.Action(player);
                }
            }
            // Cancel
            else {
                isSearching = false;
                searchingObject = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            var fieldObject = FieldObjectManager.Instance.FindClosest(transform.position, 1);
            var handheldItem = HandheldItemManager.Instance.FindClosest(transform.position, 1);
            var puzzleObstacle = PuzzleObstacleManager.Instance.FindClosest(transform.position, 1);

            if (fieldObject != null && fieldObject.item != null) {
                searchingObject = fieldObject;
                isSearching = true;
                curSearchTime = SearchDelayTime;
            }
            else if (handheldItem != null) {
                handheldItem.GetPickedUp(player);
            }
            else if (puzzleObstacle != null && player.HandheldItem != null) {
                player.HandheldItem.Action(puzzleObstacle);
            }
        }
    }
}