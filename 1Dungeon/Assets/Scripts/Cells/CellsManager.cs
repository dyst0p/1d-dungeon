using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    public static CellsManager Instance { get; private set; }

    [SerializeField] private List<BaseCell> _cells;

    public static BaseCell GetCellByIndex(int index) => Instance._cells[index];

    public static int NumberOfCells => Instance._cells.Count;

    private void Awake()
    {
        if (CellsManager.Instance != null)
            Destroy(gameObject);
        else
            CellsManager.Instance = this;
    }
}
