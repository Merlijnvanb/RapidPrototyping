using UnityEngine;

public class BallPhysics
{
    private PhysicsConfig config;
    
    private Vector3 position;
    private Vector3 velocity;
    private Quaternion rotation;

    private float currentDT;

    public BallPhysics(PhysicsConfig inConfig, Transform inTransform, float dt)
    {
        config = inConfig;
        
        position = inTransform.position;
        velocity = Vector3.zero;
        rotation = inTransform.rotation;
        
        currentDT = dt;
    }

    public void Update(float dt) // Should be called from FixedUpdate
    {
        currentDT = dt;
        
        ApplyGravity();
        UpdatePosition();
    }

    public PhysicsResult GetResult()
    {
        var result = new PhysicsResult();
        
        result.Position = position;
        result.Velocity = velocity;
        result.Rotation = rotation;

        return result;
    }

    public void SetVelocity(Vector3 vector, float force)
    {
        velocity = vector * (force * currentDT);
    }

    public void AddForce(Vector3 vector, float force)
    {
        velocity += vector * (force * currentDT);
    }

    public void RedirectVelocity(Vector3 vector, float force = 0f)
    {
        var magnitude = velocity.magnitude + (force * currentDT);
        var newVelocity = vector * magnitude;
        velocity = newVelocity;
    }

    private void ApplyGravity()
    {
        velocity.y += -9.81f * currentDT;
    }

    private void UpdatePosition()
    {
        var displacement = velocity * currentDT;
        var direction = displacement.normalized;
        var maxDist = displacement.magnitude;
        
        var hit = Physics.SphereCast(position, config.BallRadius, direction, out var hitInfo, maxDist);

        if (!hit || hitInfo.collider.CompareTag("Player"))
        {
            position += displacement;
            return;
        }

        var travelDist = hitInfo.distance - config.BallSkin;
        if (travelDist < 0)
            travelDist = 0;
        
        position += direction * travelDist;

        var normal = hitInfo.normal;
        var newVelocity = velocity - 2 * Vector3.Dot(velocity, normal) * normal;
        velocity = newVelocity * config.BounceCoefficient;
    }
}
