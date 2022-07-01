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
    private GridPosition gridPosition;

    void Awake()
    {
        targetPosition = this.transform.position;
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update() {

        if (Vector3.Distance(transform.position, targetPosition) > thresholdDistance)
        {
            unitAnimator.SetBool("IsWalking", true);
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime*rotationSpeed);

            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
        }
        
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if(newGridPosition != gridPosition)
        {
            LevelGrid.Instance.UnitMovedGridPositon(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }


}
