using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("IsHovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("IsHovered", false);
    }
}