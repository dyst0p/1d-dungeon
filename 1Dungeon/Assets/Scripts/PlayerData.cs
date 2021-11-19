using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] BaseCell _currentCell;
    public BaseCell CurrentCell
    {
        get
        {
            return _currentCell;
        }
        set
        {
            _currentCell = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
