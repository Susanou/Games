using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float thresholdDistance = .1f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private int maxMoveDistance = 4;


    private Vector3 targetPosition;
    private Unit unit;

    void Awake()
    {
        targetPosition = transform.position;
        unit = GetComponent<Unit>();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Move(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    public List<GridPosition> GetValidActionGridPosition()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int y = -maxMoveDistance; y <= maxMoveDistance; y++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
