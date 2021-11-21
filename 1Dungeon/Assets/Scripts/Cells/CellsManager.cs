using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsManager : MonoBehaviour
{
    [SerializeField] private List<BaseCell> _cells;

    public int NumberOfCells => _cells.Count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BaseCell GetCellByIndex(int index) => _cells[index];
}
