using UnityEngine;
using System;

public class BallManager : MonoBehaviourSingleton<BallManager>
{
    public GameObject BallPrefab;
    public Transform SpawnPoint;
    public Transform Parent;

    public BallHandler Ball => _ball;
    private BallHandler _ball;
    

    void Start()
    {
        _ball = Instantiate(BallPrefab, SpawnPoint.position, SpawnPoint.rotation).GetComponent<BallHandler>();
        _ball.transform.parent = Parent;
    }
}
