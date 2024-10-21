using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    public Transform camTransform;
    public float camRange;
    public float horizontal;
    public float vertical;
    public bool canMove;
    bool isMoving;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        isMoving = false;
    }
    public void Move(Vector2 context)
    {
        isMoving = true;
        Debug.Log("MoveCalled");
        Debug.Log(context.x);

        if (canMove)
        {

           
            
            horizontal = context.x;
            vertical = context.y;

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (camTransform.position.x >= camRange)
        {
            horizontal = -1;
            
        }
        if (!isMoving)
        {
            horizontal = 0;
            vertical = 0;
        }
        if (camTransform.position.y >= camRange)
        {
            vertical = -1;
        }
        if (camTransform.position.x <= -camRange)
        {
            horizontal = 1;
        }
        if (camTransform.position.y <= -camRange)
        {
            vertical = 1;
        }
        
        rB.velocity = new Vector2(horizontal * speed, rB.velocity.y);
        rB.velocity = new Vector2(rB.velocity.x, vertical * speed);

    }
}
