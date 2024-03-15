using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;//连击计数器
    private float lastTimeAttacked;//记录上一次攻击时间
    private float comboWindow = 2;//连击间隔空窗期


    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 1 || Time.time >= lastTimeAttacked + comboWindow)
        {
            comboCounter = 0;
        }

        player.anim.SetInteger("ComboCounter", comboCounter);
        stateTimer = .1f;//维持0.1s惯性效果
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;
        //攻击结束后有0.5s的僵直
        player.StartCoroutine("BusyFor", .5f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetVelocity(0, 0);
        }

        if (triggerCalled)//当动画结束时将触发器置为真，此时状态机将状态转为闲置状态
        {
            stateMachine.ChangState(player.idleState);
        }
    }
}
