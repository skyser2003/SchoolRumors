using UnityEngine;

class RitualItem : MonoBehaviour {
    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.gameObject.GetComponent<Player>();
        if (player != null) {
            player.AcquireItem(this);
            transform.localPosition = new Vector3(-9999, -9999, -9999);
        }
    }
}