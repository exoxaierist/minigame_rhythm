using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffect : MonoBehaviour
{
    public bool isFree = true;
    public Transform origin;
    private Animator animator;

    public void Effect(string name)
    {
        if (!TryGetComponent(out animator)) { Kill(); return; }
        isFree = false;
        animator.Play(name);
    }

    private void Update()
    {
        if (isFree) return;
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f) Kill();
    }

    public void Kill()
    {
        transform.SetParent(origin);
        isFree = true;
        gameObject.SetActive(false);
    }
}
