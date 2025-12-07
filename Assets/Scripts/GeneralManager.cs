using UnityEngine;

public class GeneralManager : MonoBehaviourSingleton<GeneralManager>
{
    public bool Debug;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Debug)
        {
            if (Input.GetKeyDown(KeyCode.R))
                BallManager.Instance.ResetBall();
        }
    }
}
