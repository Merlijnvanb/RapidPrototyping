using UnityEngine;

[ExecuteInEditMode]
public class GoalVolume : MonoBehaviour
{
    public Vector3 Scale;
    public Transform Visual;
    
    public bool IsInside(Vector3 point, float radius)
    {
        Vector3 local = transform.InverseTransformPoint(point);

        bool inside =
            Mathf.Abs(local.x) + radius <= Scale.x / 2f &&
            Mathf.Abs(local.y) + radius <= Scale.y / 2f &&
            Mathf.Abs(local.z) + radius <= Scale.z / 2f;

        return inside;
    }

    void Update()
    {
        Visual.localScale = Scale;
    }
}
