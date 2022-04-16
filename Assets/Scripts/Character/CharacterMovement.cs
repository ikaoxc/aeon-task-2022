using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private GamepadInputManager _inputManager;
    [SerializeField] private Transform _cameraTransform;

    [Header("Forces")] 
    [SerializeField] private float _ballAcceleration = 5.0f;
    [SerializeField] private float _ballAccelerationInAir = 1.0f;
    [SerializeField] private float _jumpAcceleration = 30.0f;
    [SerializeField] private float _jumpHoldTime = 0.5f;
    [SerializeField] private float _brakeAcceleration = 7.5f;
    [SerializeField] private float _upDotThreshold = 0.8f;

    [Header("Drag Settings")] [SerializeField]
    private float _airDrag = 0.02f;

    [SerializeField] private float _groundDragMoving = 0.1f;

    private Rigidbody _cachedRigidbody;
    private bool _grounded;

    private float _jumpTimer;
    private float _lastTouchedFriction;

    private void OnValidate()
    {
        if (_inputManager == null)
            throw new NullReferenceException($"{gameObject.name} - Input - not implemented");
        
        if (_cameraTransform == null)
            throw new NullReferenceException($"{gameObject.name} - Camera - not setted");
    }

    private void Awake()
    {
        _cachedRigidbody = GetComponent<Rigidbody>();
        _lastTouchedFriction = 0.0f;
    }

    private void FixedUpdate()
    {
        if (_inputManager == null || _cameraTransform == null)
        {
            return;
        }

        var inputState = _inputManager.AnalogueValue;
        var moving = inputState.sqrMagnitude > 0;

        if (moving)
        {
            inputState = Vector2.ClampMagnitude(inputState, 1.0f);

            var flatCameraForward = _cameraTransform.forward;
            flatCameraForward.y = 0;
            flatCameraForward.Normalize();

            var flatCameraRight = _cameraTransform.right;
            flatCameraForward.y = 0;
            flatCameraForward.Normalize();


            var ballWorldVector = (flatCameraForward * inputState.y) +
                                  (flatCameraRight * inputState.x);

            var ballAccelerationVector = ballWorldVector *
                                         (_grounded ? _ballAcceleration : _ballAccelerationInAir);

            _cachedRigidbody.AddForce(ballAccelerationVector, ForceMode.Acceleration);
        }

        var shouldJump = false;
        if (_inputManager.PrimaryButtonValue)
        {
            shouldJump = _jumpTimer > 0.0f;
        }
        else
        {
            _jumpTimer = _grounded ? _jumpHoldTime : 0.0f;
        }

        if (shouldJump)
        {
            _cachedRigidbody.AddForce(Vector3.up * _jumpAcceleration, ForceMode.Acceleration);

            _jumpTimer -= Time.fixedDeltaTime;
        }

        if (_inputManager.SecondaryButtonValue)
        {
            var brakeForce = -_cachedRigidbody.velocity.normalized * _brakeAcceleration;
            _cachedRigidbody.AddForce(brakeForce, ForceMode.Acceleration);
        }

        if (moving)
        {
            _cachedRigidbody.drag = _grounded ? _groundDragMoving : _airDrag;
        }
        else
        {
            _cachedRigidbody.drag = _grounded ? _lastTouchedFriction : _airDrag;
        }

        if (_cachedRigidbody.IsSleeping() == false)
        {
            _grounded = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckCollisionForGrounded(other);
    }

    private void OnCollisionStay(Collision other)
    {
        CheckCollisionForGrounded(other);
    }

    public void Enable()
    {
        _cachedRigidbody.isKinematic = false;
    }

    public void Disable()
    {
        _cachedRigidbody.isKinematic = true;
    }

    private bool CheckCollisionForGrounded(Collision collision)
    {
        if (collision.contacts.Length <= 0)
            return false;

        var normalSum = Vector3.zero;
        foreach (ContactPoint otherContact in collision.contacts)
        {
            normalSum += otherContact.normal;
        }

        var averageNormalized = (normalSum / collision.contacts.Length).normalized;


        if (Vector3.Dot(averageNormalized, Vector3.up) >= _upDotThreshold)
        {
            _grounded = true;
            _lastTouchedFriction = collision.collider.sharedMaterial.dynamicFriction;

            return true;
        }

        return false;
    }

}
