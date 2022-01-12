using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CellsManager : MonoBehaviour
{
    public static CellsManager Instance { get; private set; }

    [FormerlySerializedAs("_cells")] [SerializeField] private List<BaseCell> cells;

    public static BaseCell GetCellByIndex(int index) => Instance.cells[index];

    public static int NumberOfCells => Instance.cells.Count;

    private void Awake()
    {
        if (CellsManager.Instance != null)
            Destroy(gameObject);
        else
            CellsManager.Instance = this;
    }
}
