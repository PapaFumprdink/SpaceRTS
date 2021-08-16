using System;
using UnityEngine;

[SelectionBase]
[DisallowMultipleComponent]
[RequireComponent(typeof(Rigidbody))]
public sealed class ShipMovement : MonoBehaviour, IMovementComponent
{
    const float DistanceError = 0.2f;
    const float RotationError = 0.2f;

    [SerializeField] private float m_ThrustForce;
    [SerializeField] private float m_TurnPower;

    private Rigidbody m_Rigidbody;

    public Pose TargetPose { get; set; }
    public float StoppingDistance => m_Rigidbody.velocity.magnitude / (2f * m_ThrustForce);
    public float StoppingRotation => m_Rigidbody.angularVelocity.magnitude / (2f * m_TurnPower);

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        TargetPose = new Pose(transform.position, transform.rotation);
    }

    private void Update()
    {
        ApplyMovement();
        ApplyTorque();
    }

    private void ApplyMovement()
    {
        Vector3 vector = (TargetPose.position - transform.position);
        Vector3 direction = vector.normalized;

        if (vector.magnitude < m_ThrustForce * Time.deltaTime + DistanceError && m_Rigidbody.velocity.magnitude < DistanceError)
        {
            m_Rigidbody.velocity = Vector3.zero;
            transform.position = TargetPose.position;
        }
        else if (vector.magnitude < StoppingDistance)
        {
            m_Rigidbody.velocity -= direction * m_ThrustForce * Time.deltaTime;
        }
        else
        {
            m_Rigidbody.velocity += direction * m_ThrustForce * Time.deltaTime;
        }
    }

    private void ApplyTorque()
    {
        Vector3 vector = (TargetPose.rotation * Quaternion.Inverse(transform.rotation)).eulerAngles;
        Vector3 direction = vector.normalized;

        if (vector.magnitude < m_TurnPower * Time.deltaTime + RotationError && m_Rigidbody.angularVelocity.magnitude < RotationError)
        {
            m_Rigidbody.velocity = Vector3.zero;
            transform.position = TargetPose.position;
        }
        else if (vector.magnitude < StoppingRotation)
        {
            m_Rigidbody.angularVelocity -= direction * m_TurnPower * Time.deltaTime;
        }
        else
        {
            m_Rigidbody.angularVelocity += direction * m_TurnPower * Time.deltaTime;
        }
    }
}
