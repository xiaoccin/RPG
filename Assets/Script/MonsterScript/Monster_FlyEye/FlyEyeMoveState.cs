public class FlyEyeMoveState : FlyEyeGroundState
{
    
    public FlyEyeMoveState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName, Monster_FlyEye monster_FlyEye) : base(stateMachine, monster, _animBoolName,monster_FlyEye)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = monster_FlyEye.moveTime;
    }

    public override void Update()
    {
        base.Update();
        monster.SetVelocity(monster_FlyEye.moveSpeed * monster.facingDir, monster.rb.velocity.y);
        /*if( monster.IsGroundDetected()|| monster.IsWallDetected() || stateTimer < 0)
        {
            monster.Flip();
        }*/
        if(stateTimer < 0 )
        {
            monster.Flip();
            stateMachine.ChangeState(monster_FlyEye.idleState);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

}
