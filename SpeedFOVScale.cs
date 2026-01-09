// Created by Evelyn Masters on 2026-1-9

using System;
using Unity.Collections;
using UnityEngine;

// This MUST be attached to the Camera
[RequireComponent(typeof(Camera))]
public class SpeedFOVScale : MonoBehaviour
{
    [Header("Min max for FOV range")]
    public Vector2 fovRange = new(60f, 200f);

    [Header("Max Speed for full FOV")]
    [Tooltip("Based on rigidbody speed, might require tweaking")]
    public float maxSpeed = 5f;

    [Header("Curve Exponent (lower = easier to reach mid FOV)")]
    [Range(0.1f, 1f)]
    public float exponent = 0.5f;

    [Header("Current FOV Based on the Speed")]
    public float currentFOV = 90;

    private Camera cam;
    [Header("Fields")]
    public Rigidbody rb;
    public float smoothTime = 0.1f;
    
    private float fovVelocity;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        float speed = rb.linearVelocity.magnitude;
        float t = Mathf.Clamp01(speed / maxSpeed);
        float curved = Mathf.Pow(t, exponent);
        float targetFOV = Mathf.Lerp(fovRange.x, fovRange.y, curved);
        
        currentFOV = Mathf.SmoothDamp(currentFOV, targetFOV, ref fovVelocity, smoothTime);
        cam.fieldOfView = currentFOV;
    }
}
