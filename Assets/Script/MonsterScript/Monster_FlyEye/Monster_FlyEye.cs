using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FlyEye : Monster
{
    
    [Header("Move Info")]
    public float moveSpeed = 2;
    public float moveTime = 2;
    public float idleTime = 1;

    #region state
   
    public FlyEyeIdleState idleState;
    public FlyEyeMoveState moveState;
    public FlyEyeBattleState battleState;
    #endregion


    protected override void Awake()
    {
        base.Awake();
        idleState = new FlyEyeIdleState(stateMachine, this, "Idle",this);
        moveState = new FlyEyeMoveState(stateMachine, this, "Move",this);
        battleState = new FlyEyeBattleState(stateMachine, this, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        Debug.Log("start");
        stateMachine.Initialize(idleState);
    }


    protected override void Update()
    {

        base.Update();
        stateMachine.currectState.Update();
        /*Debug.Log(stateMachine.currectState.ToString());
        Debug.Log(stateMachine.currectState.GetStateTimer().ToString());*/
    }

    
}
