using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Rendering;


public class EnemyPathAI : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 430f;
    [SerializeField] private float nextWaypointDistance = 0.5f;
    [SerializeField] private SpriteRenderer spriteRenderer;

    Path path;
    int currentWaypoint = 0;
    // bool reachedEndOfPath = false;

    [SerializeField] Seeker seeker;
    [SerializeField] Rigidbody2D rb;

    private void Start()
    {
        LoadComponents();
        InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);

    }

    private void FixedUpdate()
    {

        MoveToTarget();
        UpdateVisual();

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

    private void UpdateVisual()
    {
        spriteRenderer.flipX = (target.position.x - transform.position.x) < 0;
    }

    private void MoveToTarget()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            // reachedEndOfPath = true;
            return;
        }
        else
        {
            // reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        rb.AddForce(direction * speed * Time.deltaTime);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }


    private void OnValidate()
    {
        LoadComponents();
    }
    private void LoadComponents()
    {
        if (seeker == null) seeker = GetComponent<Seeker>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
#if UNITY_EDITOR
        // Trong Editor, đừng load player (vì có thể nó chưa hiện diện)
        if (!Application.isPlaying) return;
#endif
        if (target == null) target = GameObject.FindFirstObjectByType<PlayerDamageReceiver>().transform;
    }




}
