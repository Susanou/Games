using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{

    [SerializeField] private float spinAmount = 360f;
    private float totalSpinAmount;

    private void Update() {
        if(!isActive) return;
        
        transform.eulerAngles += new Vector3(0, spinAmount*Time.deltaTime, 0);

        totalSpinAmount += spinAmount*Time.deltaTime;
        if(totalSpinAmount > 360f)
        {
            isActive = false;
            onActionComplete();
        }
        
    }

    public override void TakeAction(GridPosition position, Action onSpinComplete)
    {
        isActive = true;
        totalSpinAmount = 0f;
        this.onActionComplete = onSpinComplete;
    }

    public override string GetActionName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidActionGridPositionList()
    {
        GridPosition unitGridPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitGridPosition 
        };
    }

    public override int GetActionCost()
    {
        return 2;
    }
}
