using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControls : MonoBehaviour
{
    BattleSystem battleSystem;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAnimation();
    }

    public void MeleeAnimation()
    {
        //if (battleSystem.OnMeleeButton)
        //{
            //animator.SetTrigger("Melee");

        //}
        //animator.SetTrigger("Melee");
    }
}
