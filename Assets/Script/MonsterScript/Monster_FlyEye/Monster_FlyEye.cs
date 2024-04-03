using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_FlyEye : Monster
{
    
    [Header("Move Info")]
    public float moveSpeed = 2;
    public float moveTime = 2;
    public float idleTime = 1;

    [Header("Battle Info")]
    public float attackDistance = 1;
    public float attackCooldownTime;
    public float battleTime;

    public float lastAttackTime;

    #region state
   
    public FlyEyeIdleState idleState {  get;private set; }
    public FlyEyeMoveState moveState { get; private set; }
    public FlyEyeBattleState battleState { get; private set; }
    public FlyEyeAttackState attackState { get; private set; }
    #endregion


    protected override void Awake()
    {
        base.Awake();
        idleState = new FlyEyeIdleState(stateMachine, this, "Idle",this);
        moveState = new FlyEyeMoveState(stateMachine, this, "Move",this);
        battleState = new FlyEyeBattleState(stateMachine, this, "Move", this);
        attackState = new FlyEyeAttackState(stateMachine, this, "Attack", this);
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
        stateMachine.currentState.Update();
        /*Debug.Log(stateMachine.currectState.ToString());
        Debug.Log(stateMachine.currectState.GetStateTimer().ToString());*/
    }

    public void AnimationTragger() => stateMachine.currentState.AnimationFinishedTrigger();


}
