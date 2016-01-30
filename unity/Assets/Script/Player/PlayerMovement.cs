﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    private CharacterController control;
    private Vector3 direction = new Vector3();

    public float Speed;

    public AnimationCurve walkCurveBounce;
    public AnimationCurve walkCurveWave;
    public float animSpeed = 1.0f;
    public Transform graphics;
    Vector3 graphicsOffset;
    bool isFacingRight;

    private void Start()
    {
        control = GetComponent<CharacterController>();
        graphicsOffset = graphics.localPosition;
    }

    private void FixedUpdate()
    {
        var dt = Time.fixedDeltaTime;
        control.Move(direction * Speed * dt);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            direction.x = -1;
            isFacingRight = false;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            direction.x = 1;
            isFacingRight = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            direction.z = -1;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            direction.z = 1;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            direction.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow)) {
            direction.x = 0;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            direction.z = 0;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            direction.z = 0;
        }

        if(direction.magnitude > 0.0f)
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
    }

    float animV;
    void UpdateWalkAnim(float speed)
    {
        animV += speed * Time.deltaTime;
        if (animV > 1.0f)
        {
            animV = 0.0f;
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
    }
}
