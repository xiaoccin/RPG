using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MonsterStateMachine 
{
    public MonsterState currectState { private set; get; }

    public void  MeshChangeState(MonsterState state)
    {
        currectState.Exit();
        currectState = state;
        currectState.Enter();
    }
}
