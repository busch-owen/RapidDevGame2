using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerMovement : MonoBehaviour
{
    public static event Action OnPlayerDamaged;
    float horizontal;
    float vertical;
    public bool canMove;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rB;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Move(Vector2 context)
    {
        Debug.Log("MoveCalled");
        if (canMove)
        {
            horizontal = context.x;
            vertical = context.y;
        }
    }
    // Update is called once per frame
    void Update()
    {
        rB.velocity = new Vector2(horizontal * speed, rB.velocity.y);
        rB.velocity = new Vector2(rB.velocity.x, vertical * speed);
    }
}
