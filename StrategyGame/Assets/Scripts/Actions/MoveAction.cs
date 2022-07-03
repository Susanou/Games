using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{

    [SerializeField] private Animator unitAnimator;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float thresholdDistance = .1f;
    [SerializeField] private float rotationSpeed = 10.0f;
    [SerializeField] private int maxMoveDistance = 4;


    private Vector3 targetPosition;


    protected override void Awake()
    {
        base.Awake(); // runs the Awake() from BaseAction
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive) return;

        Vector3 moveDirection = targetPosition - transform.position;
        moveDirection.Normalize();

        if (Vector3.Distance(transform.position, targetPosition) > thresholdDistance)
        {
            unitAnimator.SetBool("IsWalking", true);
            
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            unitAnimator.SetBool("IsWalking", false);
            isActive = false;
        }

        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime*rotationSpeed);
    }

    public void Move(GridPosition targetPosition)
    {
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(targetPosition);
        isActive = true;
    }
    

    public bool IsValidActionGridPosition(GridPosition gridPosition)
    {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList()
    {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
        {
            for (int y = -maxMoveDistance; y <= maxMoveDistance; y++)
            {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if(!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                {
                    continue;
                }

                if(unitGridPosition == testGridPosition)
                {
                    //no change in position
                    continue;
                }

                if(LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    //GridPosition already occupied
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
                Debug.Log(testGridPosition);
            }
        }

        return validGridPositionList;
    }
}
