using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    public Camera PlayerCamera;
    public KeyCode FocusKey = KeyCode.Mouse1;
    public KeyCode ShotKey = KeyCode.Mouse0;

    private bool enableActions = false;

    // Update is called once per frame
    void Update()
    {
        var ball = BallManager.Instance.Ball;

        enableActions = ball.InRange(transform.position);

        if (!enableActions)
            return;
        
        if (Input.GetKey(FocusKey))
        {
            TimeManager.Instance.SlowTimeScale();
        }

        if (Input.GetKey(ShotKey))
        {
            var dir = PlayerCamera.transform.forward;
            var force = 10f;
            
            ball.Shot(dir, force);
        }
    }
}
