using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeGroundState : MonsterState
{
    protected Monster_FlyEye monster_FlyEye;
    public FlyEyeGroundState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName, Monster_FlyEye monster_FlyEye) : base(stateMachine, monster, _animBoolName)
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
        if(monster_FlyEye.IsPlayerDetected())
        {
            stateMachine.ChangeState(monster_FlyEye.battleState);
        }
    }

    
}
