using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;


public class EnemyPathAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 400f;
    [SerializeField] private float nextWaypointDistance = 0.5f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    [SerializeField] Seeker seeker;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        LoadComponents();
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);



    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {

            path = p;
            currentWaypoint = 0;
        }
    }
    private void OnValidate()
    {
        LoadComponents();
    }
    private void LoadComponents()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        rb.AddForce(direction * speed * Time.deltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }


    }




}
