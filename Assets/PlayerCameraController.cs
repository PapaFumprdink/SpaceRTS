using System;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
public sealed class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve m_PlayerMovementSpeed;
    [SerializeField] private float m_ZoomSensitivity;

    [Space]
    [SerializeField] private Cinemachine.CinemachineMixingCamera m_MixCamera;
    [SerializeField] private Transform m_InputTransform;

    private BasicControls m_ControlsAsset;
    private float m_ZoomPercent;

    private void Awake()
    {
        m_ControlsAsset = new BasicControls();
    }

    private void OnEnable()
    {
        m_ControlsAsset.Enable();
    }

    private void OnDisable()
    {
        m_ControlsAsset.Disable();
    }

    private void Update()
    {
        ReadInputs();
    }

    private void ReadInputs()
    {
        m_ZoomPercent += m_ControlsAsset.Camera.Zoom.ReadValue<float>() * m_ZoomSensitivity * Time.deltaTime;
        m_ZoomPercent = Mathf.Clamp01(m_ZoomPercent);

        MoveCamera(m_ControlsAsset.Camera.Movement.ReadValue<Vector2>());
        ApplyZoom();
    }

    private void MoveCamera(Vector2 input)
    {
        float movementSpeed = m_PlayerMovementSpeed.Evaluate(m_ZoomPercent);
        transform.position += m_InputTransform.TransformVector(input) * movementSpeed * Time.deltaTime;
    }

    private void ApplyZoom()
    {
        m_MixCamera.SetWeight(0, m_ZoomPercent);
        m_MixCamera.SetWeight(1, 1f - m_ZoomPercent);
    }
}
