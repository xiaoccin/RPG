using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeAttackState : MonsterState
{
    private Monster_FlyEye monster_FlyEye;
    public FlyEyeAttackState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName, Monster_FlyEye monster_FlyEye) : base(stateMachine, monster, _animBoolName)
    {
        this.monster_FlyEye = monster_FlyEye;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(triggerCalled == true)
        {
            monster_FlyEye.lastAttackTime = Time.time;
            stateMachine.ChangeState(monster_FlyEye.battleState);
        }
    }
}
