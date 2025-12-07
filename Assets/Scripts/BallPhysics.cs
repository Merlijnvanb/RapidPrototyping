using UnityEngine;

public class BallPhysics
{
    private const float skin = 0.01f;
    private float ballRadius;
    
    private Vector3 position;
    private Vector3 velocity;
    private Quaternion rotation;

    private float currentDT;

    public BallPhysics(Transform inTransform, float radius, float dt)
    {
        ballRadius = radius;
        currentDT = dt;
        
        position = inTransform.position;
        velocity = Vector3.zero;
        rotation = inTransform.rotation;
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
        velocity = vector * force;
    }

    public void AddForce(Vector3 vector, float force)
    {
        velocity += vector * force;
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
        
        var hit = Physics.SphereCast(position, ballRadius, direction, out var hitInfo, maxDist);

        if (!hit)
        {
            position += displacement;
            return;
        }

        var travelDist = hitInfo.distance - skin;
        if (travelDist < 0)
            travelDist = 0;
        
        position += direction * travelDist;

        var normal = hitInfo.normal;
        var newVelocity = velocity - 2 * Vector3.Dot(velocity, normal) * normal;
        velocity = newVelocity;
    }
}
