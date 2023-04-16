using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationControls : MonoBehaviour
{
    public Animator animator;
    public Button meleeButton;
    public Button rangedButton;
    public Button healButton;
    public Button specialButton;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Melee");
        animator.SetTrigger("Ranged");
        animator.SetTrigger("Special");
    }
}
