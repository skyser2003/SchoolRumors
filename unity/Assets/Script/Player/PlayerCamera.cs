using UnityEngine;

class PlayerCamera : MonoBehaviour {

    public Player player;
    public Vector3 DeltaVector;
    public float PosFactor = 0.5f;
    public Vector3 DeltaLookVector;

    private void FixedUpdate()
    {
        Vector3 newpos = Vector3.Lerp(transform.position, player.transform.position + DeltaVector, PosFactor);
        transform.position = newpos;
    }

    void LateUpdate()
    {
        //look at
        transform.LookAt(player.transform.position + DeltaLookVector);
    }
}