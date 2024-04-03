using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MonsterStateMachine 
{
    public MonsterState currectState { private set; get; }

    public void Initialize(MonsterState state)
    {
        currectState = state;
        currectState.Enter();
    }


    public void  ChangeState(MonsterState state)
    {
        currectState.Exit();
        currectState = state;
        currectState.Enter();
    }
}
