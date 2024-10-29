using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{

    private EventManager _eventManager;

    public GameObject end;
    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        end.SetActive(false);
        _eventManager._Ended += EndDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDay()
    {
        end.SetActive(true);
    }
}
