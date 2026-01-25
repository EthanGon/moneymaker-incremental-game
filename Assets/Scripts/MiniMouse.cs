using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMouse : MonoBehaviour
{
    private Animator anim;
    private float timer;
    public float cd;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (timer < cd)
        {
            timer += Time.deltaTime;
        } 
        else
        {
            timer = 0;
            StartCoroutine(TriggerClickAnimation());
        }
    }

    private IEnumerator TriggerClickAnimation()
    {
        anim.SetTrigger("click");
        yield return new WaitForSeconds(1.2f);
        anim.SetTrigger("off-click");
    }
}
