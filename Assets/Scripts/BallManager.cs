using UnityEngine;
using System;

public class BallManager : MonoBehaviourSingleton<BallManager>
{
    public GameObject BallPrefab;
    public Transform SpawnPoint;
    public Transform Parent;

    public GoalVolume Goal;

    public BallHandler Ball => _ball;
    private BallHandler _ball;
    

    void Start()
    {
        SpawnBall();
    }

    public void ResetBall()
    {
        Destroy(_ball.gameObject);
        SpawnBall();
    }

    public void CheckGoal(Vector3 pos)
    {
        var scored = Goal.IsInside(pos, _ball.BallRadius);
        if (scored)
        {
            Debug.Log("Scored!");
            ResetBall();
        }
    }

    private void SpawnBall()
    {
        _ball = Instantiate(BallPrefab, SpawnPoint.position, SpawnPoint.rotation).GetComponent<BallHandler>();
        _ball.transform.parent = Parent;
    }
}
