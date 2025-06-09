using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected Sprite spriteReadyForUse;
    protected Sprite spriteGeneral;
    protected SpriteRenderer spriteRenderer;
    protected Canvas help;

    public abstract void Use(GameObject whoUsed);

    protected virtual void Start()
    {
        spriteGeneral = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
        help = GetComponentInChildren<Canvas>(true);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && spriteReadyForUse != null)
        {
            spriteRenderer.sprite = spriteReadyForUse;
            help.enabled = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && spriteGeneral != null)
        {
            spriteRenderer.sprite = spriteGeneral;
            help.enabled = false;
        }
    }
}