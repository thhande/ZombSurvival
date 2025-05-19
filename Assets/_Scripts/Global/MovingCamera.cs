using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float moveSpeed = 15f;


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

        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }
}
