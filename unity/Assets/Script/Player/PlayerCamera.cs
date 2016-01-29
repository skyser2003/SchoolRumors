using UnityEngine;

class PlayerCamera : MonoBehaviour {
    public Player player;

    private void FixedUpdate()
    {
        transform.position = player.transform.position + new Vector3(0, 0.81f, -1.81f);
    }
}