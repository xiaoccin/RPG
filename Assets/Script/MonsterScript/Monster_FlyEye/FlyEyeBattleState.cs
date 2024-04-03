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
        player = GameObject.Find("Player").transform;
        Debug.Log("enter battle");
    }

    public override void Update()
    {
        base.Update();
        if(player != null)
        {
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

    
}
