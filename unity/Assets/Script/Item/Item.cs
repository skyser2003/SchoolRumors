using UnityEngine;

class Item : MonoBehaviour {
    private void OnTriggerEnter(Collider collider)
    {
        var player = collider.gameObject.GetComponent<Player>();
        if (player != null) {

        }
    }
}