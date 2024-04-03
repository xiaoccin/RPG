

using System.Diagnostics;
using UnityEngine;

public class FlyEyeIdleState : FlyEyeGroundState
{

    public FlyEyeIdleState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName, Monster_FlyEye monster_FlyEye) : base(stateMachine, monster, _animBoolName, monster_FlyEye)
    {
        
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = monster_FlyEye.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(monster_FlyEye.moveState);
        }
    }
}
