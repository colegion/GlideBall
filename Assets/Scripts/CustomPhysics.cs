using Scriptables;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CustomPhysics : MonoBehaviour
{
    [Header("Physics Properties")]
    [SerializeField] private float mass = 1f;
    [SerializeField] private float friction = 0.1f;
    [SerializeField] private float bounciness = 0.1f;
    [SerializeField] private CustomPhysicsProperties physicsProperties;

    [Header("Debug Options")] 
    [SerializeField] private bool debugCollision = true;

    [SerializeField] private GameObject rocketman;
    [SerializeField] private float rotationFactor;
    [SerializeField] private float tiltBorder;

    private Vector3 _velocity;
    private bool _isGrounded;
    private Collider _objectCollider;
    private bool _canRotate;
    private bool _isGliding;
    private float _glideAmount;

    private bool _enablePhysics = false;

    private void Start()
    {
        _objectCollider = GetComponent<Collider>();
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
            _velocity += Vector3.down * (physicsProperties.gravity * Time.fixedDeltaTime);
        }
    }

    private void ApplyFriction()
    {
        if (_isGrounded)
        {
            Vector3 horizontalVelocity = new Vector3(_velocity.x, 0, _velocity.z);
            Vector3 frictionForce = horizontalVelocity * (-friction * Time.fixedDeltaTime);
            _velocity += frictionForce;
            if (horizontalVelocity.magnitude < friction * Time.fixedDeltaTime)
            {
                _velocity.x = 0;
                _velocity.z = 0;
            }
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
            
            rocketman.transform.Rotate(new Vector3(0f, _glideAmount, _glideAmount) * Time.fixedDeltaTime, Space.Self);
            Vector3 currentEuler = rocketman.transform.localEulerAngles;
            rocketman.transform.localEulerAngles = new Vector3(90f, currentEuler.y, currentEuler.z);

            Debug.Log($"Updated Rotation: {rocketman.transform.rotation.eulerAngles}");
        }
        else
        {
            if (_canRotate)
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
        _isGrounded = contactNormal.y > 0.5f;

        if (debugCollision)
        {
            Debug.Log($"Collision Normal: {contactNormal}, Reflection: {reflectionVector}, Velocity: {_velocity}");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 contactNormal = collision.contacts[0].normal;
        _isGrounded = contactNormal.y > 0.5f;
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

    public void SetCanRotate(bool value)
    {
        _canRotate = value;
    }

    public void SetIsGliding(bool value)
    {
        _isGliding = value;
    }

    public void SetGlideAmount(float amount)
    {
        _glideAmount = amount;
    }
}