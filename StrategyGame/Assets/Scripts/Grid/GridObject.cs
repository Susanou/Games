using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{

    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> unitList;

    public GridObject(GridSystem system, GridPosition position)
    {
        this.gridSystem = system;
        this.gridPosition = position;
        unitList = new List<Unit>();
    }

    public override string ToString()
    {
        string unitString = "";
        
        foreach(Unit unit in unitList)
        {
            unitString += unit + "\n";
        }


        return gridPosition.ToString() + "\n" + unitString;
    }

    public List<Unit> GetUnitList()
    {
        return this.unitList;
    }

    public void AddUnit(Unit unit)
    {
        unitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        unitList.Remove(unit);
    }

    public bool HasAnyUnit()
    {
        return unitList.Count > 0;
    }
}
