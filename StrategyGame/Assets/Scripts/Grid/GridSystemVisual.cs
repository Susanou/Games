using System;
using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance {get; private set;}

    [SerializeField] private Transform gridSystemVisualSinglePrefab;
    private GridSystemVisualSingle[,] gridSystemVisualSingleArray;


    private void Awake() {
        if (Instance != null)
        {
            Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    private void Start()
    {
        gridSystemVisualSingleArray = new GridSystemVisualSingle[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++)
        {
            for(int z = 0; z < LevelGrid.Instance.GetHeight(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform grisSystemVisualSingleTransform = 
                    Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);

                gridSystemVisualSingleArray[x, z] = grisSystemVisualSingleTransform.GetComponent<GridSystemVisualSingle>();
            }
        }
    }

    private void Update() {
        UpdateGridVisual();
    }

    public void HideAllGridPosition()
    {
        foreach(GridSystemVisualSingle visual in gridSystemVisualSingleArray)
        {
            visual.Hide();
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList)
    {
        foreach(GridPosition position in gridPositionList)
        {
            gridSystemVisualSingleArray[position.x, position.z].Show();
        }
    }

    private void UpdateGridVisual()
    {
        HideAllGridPosition();

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();

        ShowGridPositionList(selectedUnit.GetMoveAction().GetValidActionGridPositionList());
    }
}
