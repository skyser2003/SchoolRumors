using UnityEngine;

class Exit : MonoBehaviour {
    public Floor floor;

    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.gameObject.GetComponent<Player>();
        if (player != null) {
            floor.ProceedToNextFloor(player);
        }
    }
}