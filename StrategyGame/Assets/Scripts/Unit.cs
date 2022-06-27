using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public float moveSpeed;
    public float thresholdDistance;

    private Vector3 targetPosition;

    private void Update() {

        if (Vector3.Distance(transform.position, targetPosition) > thresholdDistance)
        {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
        }

        
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
