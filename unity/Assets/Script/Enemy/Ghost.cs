using UnityEngine;
using System.Collections;

public class Ghost : Enemy
{
    public Texture patrolTexture;
    public Texture chaseTexture;
    MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        meshRenderer.material.mainTexture = currentState == PatrolState.patrol ? patrolTexture : chaseTexture;
    }

}
