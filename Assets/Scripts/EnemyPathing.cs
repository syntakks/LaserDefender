using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    private WaveConfig waveConfig; 
    List<Transform> waypoints;
    float moveSpeed = 2f; 
    int waypointIndex = 0;

    // Setup Enemy
    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
        waypoints = waveConfig.GetWaypoints();
        moveSpeed = waveConfig.MoveSpeed;
        transform.position = waypoints[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMove(); 
    }

    private void CheckMove()
    {
        if (waypoints.Count < 2) return; 

        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].transform.position;
            float movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            CheckPositionMatch(targetPosition);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    private void CheckPositionMatch(Vector3 targetPosition)
    {
        if (transform.position == targetPosition)
        {
            waypointIndex++; 
        }
    }
}
