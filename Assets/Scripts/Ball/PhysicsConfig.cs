using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsConfig", menuName = "Scriptable Objects/PhysicsConfig")]
public class PhysicsConfig : ScriptableObject
{
    [SerializeField] private float ballRadius = 0.5f;
    public float BallRadius => ballRadius;
    
    [SerializeField] private float ballSkin = 0.01f;
    public float BallSkin => ballSkin;
    
    [SerializeField] private float bounceCoefficient = 0.8f;
    public float BounceCoefficient => bounceCoefficient;
}
