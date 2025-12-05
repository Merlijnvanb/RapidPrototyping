using UnityEngine;

public struct PhysicsResult
{
    public Vector3 Position;
    public Vector3 Velocity;
    public Quaternion Rotation;

    public bool Collided;
}


public class BallHandler : MonoBehaviour
{
    public float BallRadius;
    public float InteractRange;
    public bool InRange(Vector3 other) => (other - transform.position).magnitude < InteractRange;

    private BallPhysics physics;
    private PhysicsResult currentPR;
    private PhysicsResult previousPR;
    private float lastFixedUpdateTime;

    void Start()
    {
        physics = new BallPhysics(transform, BallRadius, Time.fixedDeltaTime);
        
        currentPR = physics.GetResult();
        previousPR = physics.GetResult();
    }
    
    void Update()
    {
        var t = (Time.time - lastFixedUpdateTime) / Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(previousPR.Position, currentPR.Position, t);
    }

    void FixedUpdate()
    {
        physics.Update(Time.fixedDeltaTime);
        
        previousPR = currentPR;
        currentPR = physics.GetResult();

        lastFixedUpdateTime = Time.time;
    }

    public void Shot(Vector3 direction, float power)
    {
        physics.SetVelocity(direction, power);
    }
}
