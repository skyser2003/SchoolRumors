using UnityEngine;
using System.Collections;

public class Ghost : Enemy
{
    public Texture patrolTexture;
    public Texture chaseTexture;
    MeshRenderer meshRenderer;

    public AudioClip patrolAudio;
    public AudioClip chaseAudio;
    AudioSource audioSource;

    void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        audioSource = GetComponentInChildren<AudioSource>();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        meshRenderer.material.mainTexture = currentState == PatrolState.patrol ? patrolTexture : chaseTexture;

        if (currentState == PatrolState.patrol && audioSource.clip != patrolAudio)
        {
            audioSource.clip = patrolAudio;
            audioSource.Play();
        }
        else if (currentState == PatrolState.chase && audioSource.clip != chaseAudio)
        {
            audioSource.clip = chaseAudio;
            audioSource.Play();
        }
    }

}
