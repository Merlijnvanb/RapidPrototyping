using UnityEngine;

public class TimeManager : MonoBehaviourSingleton<TimeManager>
{
    public float MaxTimeScale = 1f;
    public float MinTimeScale = .1f;
    public float SlowDownSpeed = .5f;
    
    private float slowDownT;
    private bool isSlowed = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = MaxTimeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSlowed && slowDownT < 1f)
        {
            slowDownT += Time.unscaledDeltaTime * SlowDownSpeed;
            slowDownT = Mathf.Clamp01(slowDownT);
        }
        
        if (!isSlowed)
        {
            slowDownT = 0f;
        }
        
        Time.timeScale = Mathf.Lerp(MaxTimeScale, MinTimeScale, slowDownT);
        //Debug.Log("Current timescale: " + Time.timeScale);

        isSlowed = false;
    }

    public void SlowTimeScale()
    {
        isSlowed = true;
    }
}
