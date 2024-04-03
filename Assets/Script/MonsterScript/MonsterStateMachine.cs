using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MonsterStateMachine 
{
    public MonsterState currentState { private set; get; }

    public void Initialize(MonsterState state)
    {
        currentState = state;
        currentState.Enter();
    }


    public void  ChangeState(MonsterState state)
    {
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
}
