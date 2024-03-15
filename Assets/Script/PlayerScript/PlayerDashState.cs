using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;//���ó�̼�ʱ��
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.dashSpeed * player.dashDir, 0);//y����Ϊ0������ʱ����

        if (stateTimer < 0)//��ʱ��Ϊ0�����ص�����״̬
        {
            stateMachine.ChangState(player.idleState);
        }
    }
}
