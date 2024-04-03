using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEyeAnimationTriggers : MonoBehaviour
{
    private Monster_FlyEye monster_FlyEye=> GetComponentInParent<Monster_FlyEye>();

    private void AnimationTrigger()
    {
        monster_FlyEye.AnimationTragger();
    }
}
