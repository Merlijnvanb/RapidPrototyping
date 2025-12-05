using UnityEngine;

public class BallPhysics
{
    private Vector3 position;
    private Vector3 velocity;
    private Quaternion rotation;

    private float currentDT;

    public BallPhysics(Transform inTransform, float dt)
    {
        currentDT = dt;
        
        position = inTransform.position;
        velocity = Vector3.zero;
        rotation = inTransform.rotation;
    }

    public void Update(float dt) // Should be called from FixedUpdate
    {
        currentDT = dt;
        
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

    private void UpdatePosition()
    {
        position += velocity * currentDT;
    }
}
