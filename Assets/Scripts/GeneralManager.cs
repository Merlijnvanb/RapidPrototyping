using UnityEngine;

public class GeneralManager : MonoBehaviourSingleton<GeneralManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
