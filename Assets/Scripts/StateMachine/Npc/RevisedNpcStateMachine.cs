using System.Collections;
using System.Collections.Generic;
using Code.Scripts.StateMachine;
using UnityEngine;

public class RevisedNpcStateMachine : BaseStateMachine
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public void CheckShelves()
    {
        //for each row in shelf *
        //for each item in items *
        // check the item wanted and budget agaisnt the item in the current row.
        //if found check the quantity of the item
        // if found and quantity matches move to next row and next item
        
        //if no next item move to check out state*
        
        //if not found check the next row 
        //if all rows yeild no results or the budget and item don't match -- from till leave*
        
        // move to angry reaction state if none found within budget
        
        //move to happy reaction state if all found within budget
        
        // if before leave > 1 move back into wander state from the reaction state and onto the next adjecent shelf to check for more items.
        // if the above check runs and before leave <= 0 check to see if items are held
        // if items are held enter checking out state*
        // if not enter exit state*
        
    }

    public void InstantiateItemToSwipe()
    {
        //for each item in npc items held
        // instantiate an item to swipe with it's itemSo set to the itemSo currently selected in the foreach loop
        //parent this item to a point on the register to scan.
        
        // on trigger enter with scan point per scan point when the item enters increment the points scaned, once that number equals that of the number of scan points, mark the operation as complete
        // at the end of the scan run a random between 1-2 to see if the scan succeeded
        // if success play win sfx if lose play lose sfx, change led based on succeed or fail
    }

    public void ChooseRowToStock(Rows row)
    {
        //
    }
}
