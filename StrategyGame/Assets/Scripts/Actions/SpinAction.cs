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
        }
        
    }

    public void Spin()
    {
        isActive = true;
        totalSpinAmount = 0f;
    }
}
