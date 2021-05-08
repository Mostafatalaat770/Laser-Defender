using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPath : MonoBehaviour
{
    WaveConfig waveConfig;
    List<Transform> waypoints;

    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.getWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    public void setWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.MoveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position,
                targetPos,
                movementThisFrame);

            if (transform.position == targetPos)
            {
                ++waypointIndex;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
