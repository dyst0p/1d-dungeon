using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _startCell;
    [SerializeField] private GameObject _standartCell;

    [SerializeField] private int _chunkSize = 100;
    private int _borderPosition;

    private PlayerData _player;

    void Start()
    {
        _player = FindObjectOfType<PlayerData>();
        _borderPosition = _chunkSize / 2;
        StartCoroutine(nameof(SpawnChunk));
    }

    void Update()
    {
        if (_player.currentCell.Index >= _borderPosition)
        {
            StartCoroutine(nameof(SpawnChunk));
            _borderPosition += _chunkSize;
        }
    }

    private IEnumerator SpawnChunk()
    {
        Debug.Log("StartCoroutine: SpawnChunk");
        for (int i = 0; i < _chunkSize; i++)
        {
            var cellsCount = CellsManager.NumberOfCells;
            if (cellsCount == 0)
            {
                SpawnStartCell();
                yield return null;
            }
            else
            {
                SpawnStandartCell(cellsCount);
                yield return null;
            }

        }
    }

    private void SpawnStartCell()
    {
        var cell = Instantiate(_startCell);
        var cellScript = cell.GetComponent<BaseCell>();
        cellScript.Unit = _player.gameObject;
        _player.currentCell = cellScript;
        CellsManager.AddCell(cellScript);
        Debug.Log($"Spawned start cell {cell}");
    }

    private void SpawnStandartCell(int index)
    {
        var position = new Vector3(0, 0, index * 2);
        var cell = Instantiate(_standartCell, position, Quaternion.identity);
        var cellScript = cell.GetComponent<BaseCell>();
        cellScript.Index = index;
        CellsManager.AddCell(cellScript);
    }
}
