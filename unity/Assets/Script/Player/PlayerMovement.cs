using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private CharacterController control;
    private Vector3 direction = new Vector3();
    public Vector3 Direction { get { return direction; } }

    public float Speed;

    public AnimationCurve walkCurveBounce;
    public AnimationCurve walkCurveWave;
    public float animSpeed = 1.0f;
    public Transform graphics;
    Vector3 graphicsOffset;
    bool isFacingRight;
    bool isCrouching;

    public AudioSource audioFoot;
    float footStepTimer;
    float footStepRate = 0.25f;
    public AudioSource audioCrouch;

    public Texture standTexture;
    public Texture crouchTexture;
    MeshRenderer meshRenderer;

    public Transform rightHand;
    private Vector3 rightHandOffset;

    private void Start()
    {
        ChangeFloor.lastTeleportPos = transform.position;
        control = GetComponent<CharacterController>();
        graphicsOffset = graphics.localPosition;
        rightHandOffset = rightHand.localPosition;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (Cutscene.isInCutscene || isCrouching)
            return;

        var dt = Time.fixedDeltaTime;
        control.Move(direction * Speed * dt);
    }

    private void Update()
    {
        if (Cutscene.isInCutscene)
        {
            direction = Vector3.zero;
            return;
        }
            
        if (Input.GetKey(KeyCode.LeftArrow)) {
            direction.x = -1;
            isFacingRight = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow)) {
            direction.x = 1;
            isFacingRight = true;
        }

        if (Input.GetKey(KeyCode.DownArrow)) {
            direction.z = -1;
        }
        else if (Input.GetKey(KeyCode.UpArrow)) {
            direction.z = 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            direction.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            direction.x = 0;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            direction.z = 0;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow)) {
            direction.z = 0;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            control.height = isCrouching ? 0.5f : 1.01f;
            meshRenderer.material.mainTexture = isCrouching ? crouchTexture : standTexture;

            audioCrouch.Play();
        }


        if (direction.magnitude > 0.0f && !isCrouching)
        {
            UpdateWalkAnim(animSpeed);
        }
        else
        {
            UpdateWalkAnim(0.0f);
        }
    }

    void LateUpdate()
    {
        graphics.localScale = new Vector3(isFacingRight ? 1.0f : -1.0f, 1.0f, 1.0f);

        var newHandPosition = rightHand.localPosition;
        newHandPosition.x = Mathf.Abs(rightHand.localPosition.x) * (isFacingRight == true ? 1 : -1);

        rightHand.localPosition = newHandPosition;
        rightHand.localScale = new Vector3(isFacingRight == true ? 1 : -1, 1, 1);
    }

    float animV;
    void UpdateWalkAnim(float speed)
    {
        animV += speed * Time.deltaTime;
        if (animV > 1.0f)
        {
            animV = 0.0f;
        }

        if (speed > 0.0f && audioFoot != null)
        {
            footStepTimer += Time.deltaTime;

            if (footStepTimer > footStepRate)
            {
                footStepRate = 0.25f * Random.Range(0.95f, 1.05f);
                footStepTimer = 0.0f;
                audioFoot.pitch = Random.Range(0.9f, 1.2f);
                audioFoot.Play();
            }
        }

        if (speed <= 0.0f)
        {
            if (animV > 0.25f)
            {
                animV = Mathf.Lerp(animV, 0.75f, Time.deltaTime * 10.0f);
            }
            else
            {
                animV = Mathf.Lerp(animV, 0.25f, Time.deltaTime * 10.0f);
            }
        }

        graphics.localPosition = graphicsOffset + Vector3.up * walkCurveBounce.Evaluate(animV);
        graphics.eulerAngles = new Vector3(0.0f, 0.0f, walkCurveWave.Evaluate(animV) * 10.0f);

        rightHand.localPosition = rightHandOffset + Vector3.up * walkCurveBounce.Evaluate(animV);
        rightHand.eulerAngles = new Vector3(0.0f, 0.0f, walkCurveWave.Evaluate(animV) * 10.0f);
    }
}
