using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    private Animator animator;

    private readonly int hashUp = Animator.StringToHash("Up");

    private bool isMove = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void HandleAnim()
    {
        if (isMove == true) return;
        if (EnemySpawner.Instance.isStage() == true) return;
        Debug.Log("Start Handle Animation");
        animator.SetTrigger(hashUp);
        isMove = true;
    }

    public void HandleUp()
    {
        Debug.Log("Start Stage");
        GameManager.Instance.StartStage();
    }

    public void HandleDown()
    {
        isMove = false;
    }
}
