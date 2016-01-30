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
            var obj = FieldObjectManager.Instance.FindClosest(transform.position, 1);
            if (obj != null) {
                obj.GiveItem(player);
            }
        }
    }
}