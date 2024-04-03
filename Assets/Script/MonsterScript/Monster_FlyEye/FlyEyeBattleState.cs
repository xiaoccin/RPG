using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeBattleState : MonsterState
{
    protected Monster_FlyEye monster_FlyEye;
    protected Transform player;
    private int moveDir;
    public FlyEyeBattleState(MonsterStateMachine stateMachine, Monster monster, string _animBoolName, Monster_FlyEye monster_FlyEye) : base(stateMachine, monster, _animBoolName)
    {
        this.monster_FlyEye = monster_FlyEye;
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = monster_FlyEye.battleTime;
        player = GameObject.Find("Player").transform;
        Debug.Log("enter battle");
    }

    public override void Update()
    {
        base.Update();
        if(player != null)
        {
            if (monster_FlyEye.IsPlayerDetected())
            {
                if (monster_FlyEye.IsPlayerDetected().distance < monster_FlyEye.attackDistance && CanAttack())
                {
                    stateMachine.ChangeState(monster_FlyEye.attackState);
                }
            }
            else
            {
                if (stateTimer < 0)
                {
                    stateMachine.ChangeState(monster_FlyEye.idleState);
                }
            }
            


            if(player.position.x > monster.transform.position.x)
            {
                moveDir = 1;
            }
            else
            {
                moveDir = -1;
            }
        }
        monster.SetVelocity(moveDir  * monster_FlyEye.moveSpeed , monster.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public bool CanAttack()
    {
        if(monster_FlyEye.lastAttackTime + monster_FlyEye.attackCooldownTime < Time.time)
        {
            return true;
        }
        return false;
    }

}
