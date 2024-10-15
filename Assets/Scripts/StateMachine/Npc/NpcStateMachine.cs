using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;


public enum NpcStateName
{
    Enter,Wander,CheckShelf,CorrectItem,IncorrectItem,Dialog,Checkout,Exit
}
public class NewBehaviourScript : BaseStateMachine
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(NpcStateName stateName)
    {
        switch (stateName)
        {
            //case NpcStateName.Checkout:
                //base.ChangeState();
                
        }
    }
}
