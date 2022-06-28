using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private float moveSpeed;
    [SerializeField] private float thresholdDistance;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator unitAnimator;

    private Vector3 targetPosition;

    private void Update() {

        if (Vector3.Distance(transform.position, targetPosition) > thresholdDistance)
        {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime*rotationSpeed);

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move(MouseWorld.GetPosition());
            unitAnimator.SetBool("IsWalking", true);
        }

        
    }

    private void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
}
