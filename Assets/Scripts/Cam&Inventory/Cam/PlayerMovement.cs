using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    public Transform camTransform;
    [SerializeField] float camRange;
    public float Horizontal;
    public float Vertical;
    public bool CanMove;
    bool isMoving;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        CanMove = true;
        isMoving = false;
    }
    public void Move(Vector2 context)
    {
        isMoving = true;
        Debug.Log("MoveCalled");
        Debug.Log(context.x);

        if (CanMove)
        {

           
            
            Horizontal = context.x;
            Vertical = context.y;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            Horizontal = 0;
            Vertical = 0;
        }
        if (camTransform.position.x >= camRange)
        {
            Horizontal = -1;            
        }
        if (camTransform.position.y >= camRange)
        {
            Vertical = -1;
        }
        if (camTransform.position.x <= -camRange)
        {
            Horizontal = 1;
        }
        if (camTransform.position.y <= -camRange)
        {
            Vertical = 1;
        }
        
        rB.velocity = new Vector2(Horizontal * speed, rB.velocity.y);
        rB.velocity = new Vector2(rB.velocity.x, Vertical * speed);

    }
}
