using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{

    private EventManager _eventManager;

    public GameObject end;
    // Start is called before the first frame update
    void Start()
    {
        _eventManager = FindFirstObjectByType<EventManager>();
        _eventManager._Ended += EndDay;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndDay()
    {
        SceneManager.LoadScene("End");
    }
}
