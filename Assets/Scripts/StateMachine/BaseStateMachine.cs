using System;
using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public abstract class BaseStateMachine : MonoBehaviour
    {
        protected IState CurrentState { get; private set; }

        public virtual void ChangeState(IState newState)
        {
            if (newState == CurrentState)
                return;
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }

        public virtual void Update()
        {
            CurrentState?.Update();
        }
        
        public virtual void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }

        public virtual void OnTriggerEnter2D(Collider2D other)
        {
            
        }
    }
}