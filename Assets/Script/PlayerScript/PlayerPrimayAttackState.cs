using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;//����������
    private float lastTimeAttacked;//��¼��һ�ι���ʱ��
    private float comboWindow = 2;//��������մ���


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
        stateTimer = .1f;//ά��0.1s����Ч��
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;
        //������������0.5s�Ľ�ֱ
        player.StartCoroutine("BusyFor", .5f);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            player.SetVelocity(0, 0);
        }

        if (triggerCalled)//����������ʱ����������Ϊ�棬��ʱ״̬����״̬תΪ����״̬
        {
            stateMachine.ChangState(player.idleState);
        }
    }
}
