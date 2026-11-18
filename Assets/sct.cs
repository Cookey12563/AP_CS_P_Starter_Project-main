using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform2D : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 3f;
    public bool isON = true;

    [SerializeField] private List<Transform> waypoints = new List<Transform>();
    private int waypointIndex = 0;

    void Update()
    {
        if (!isON || waypoints.Count == 0) return;
        Vector2 targetPosition = waypoints[waypointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Count;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            collision.transform.SetParent(null);
        }
    }
}
