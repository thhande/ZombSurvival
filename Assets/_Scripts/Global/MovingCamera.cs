using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float maxDis = 1.5f;

    void Start()
    {
        LoadTargetTransform();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowTarget();
    }
    private void LoadTargetTransform()
    {
        target = GameObject.FindObjectOfType<PlayerMovement>().transform;
    }

    private void OnValidate()
    {

    }

    private void FollowTarget()
    {
        if (target == null) return;
        if (transform.position == target.transform.position) return;
        Vector2 dir = target.transform.position - transform.position;
        if (dir.magnitude > maxDis)
        {
            transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);
        }

    }
}
