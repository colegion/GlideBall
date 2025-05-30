using System;
using Scriptables;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CustomPhysics : MonoBehaviour
{
    [Header("Physics Properties")]
    [SerializeField] private float mass = 1f;
    [SerializeField] private float bounciness = 0.1f;
    [SerializeField] private CustomPhysicsProperties physicsProperties;
    [SerializeField] private GameObject rocketman;
    [Header("Physics Factors")]
    [SerializeField] private float rotationFactor;
    [SerializeField] private float glideFactor;
    [SerializeField] private float glideVelocityFactor;

    private Vector3 _velocity;
    private bool _isGrounded;
    private Collider _objectCollider;
    private bool _wingsEnabled;
    private bool _isGliding;
    private float _glideAmount;
    private bool _enablePhysics = false;

    public static event Action OnBallGrounded;

    private void Start()
    {
        _objectCollider = GetComponent<Collider>();
        physicsProperties.currentGravity = physicsProperties.defaultGravity;
    }

    private void FixedUpdate()
    {
        if (!_enablePhysics)
        {
            return;
        }

        ApplyGravity();
        ApplyFriction();
        HandleMovement();
        Rotate();
    }

    private void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _velocity += Vector3.down * (physicsProperties.currentGravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyFriction()
    {
        if (_isGrounded)
        {
            Vector3 horizontalVelocity = new Vector3(_velocity.x, 0, _velocity.z);
            Vector3 frictionForce = horizontalVelocity * (-physicsProperties.friction * Time.fixedDeltaTime);
            _velocity += frictionForce;
            if (horizontalVelocity.magnitude < physicsProperties.friction * Time.fixedDeltaTime)
            {
                _velocity.x = 0;
                _velocity.z = 0;
            }

            _enablePhysics = false;
            OnBallGrounded?.Invoke();
        }
    }

    private void HandleMovement()
    {
        transform.Translate(_velocity * Time.fixedDeltaTime, Space.World);
    }
    
    private void Rotate()
    {
        if (_isGliding)
        {
            Debug.Log($"Glide Amount: {_glideAmount}");
            
            rocketman.transform.Rotate(new Vector3(0f, -_glideAmount, -_glideAmount) * (Time.fixedDeltaTime * glideFactor), Space.Self);
            Vector3 currentEuler = rocketman.transform.localEulerAngles;
            rocketman.transform.localEulerAngles = new Vector3(90f, currentEuler.y, currentEuler.z);
            _velocity += new Vector3(_glideAmount * glideVelocityFactor, 0, 0);

            Debug.Log($"Updated Rotation: {rocketman.transform.rotation.eulerAngles}");
        }
        else
        {
            if (_wingsEnabled)
            {
                Vector3 currentEuler = rocketman.transform.localEulerAngles;
                rocketman.transform.localEulerAngles = new Vector3(90f, currentEuler.y, currentEuler.z);
            }
            else
            {
                rocketman.transform.Rotate(new Vector3(_velocity.z * rotationFactor, 0f, 0f) * Time.fixedDeltaTime, Space.Self);
            }
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 contactNormal = collision.contacts[0].normal;
        Vector3 reflectionVector = Vector3.Reflect(_velocity, contactNormal);
        _velocity = reflectionVector * bounciness;
        _isGrounded = contactNormal.y > 0.5f && !IsCollidedWithPlatform(collision);
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 contactNormal = collision.contacts[0].normal;
        _isGrounded = contactNormal.y > 0.5f && !IsCollidedWithPlatform(collision);
    }

    private void OnCollisionExit(Collision collision)
    {
        _isGrounded = false;
    }

    public void AddForce(Vector3 force)
    {
        _enablePhysics = true;
        _velocity += force / mass;
    }

    public void SetWingsStatus(bool value)
    {
        if (value)
        {
            physicsProperties.currentGravity = physicsProperties.defaultGravity / 2f;
        }
        else
        {
            physicsProperties.currentGravity = physicsProperties.defaultGravity;
        }
        _wingsEnabled = value;
    }

    public void SetIsGliding(bool value)
    {
        _isGliding = value;
    }

    public void SetGlideAmount(float amount)
    {
        _glideAmount = amount;
    }

    public bool IsCollidedWithPlatform(Collision collision) =>
        collision.gameObject.TryGetComponent(out Platform platform);
}