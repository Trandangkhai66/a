using UnityEngine;

public class ColoredFlash : MonoBehaviour
{
    public Material flashMaterial;
    public float duration = 0.125f;
    public SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private void Start()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        spriteRenderer.material = flashMaterial;
        yield return new WaitForSeconds(duration);
        spriteRenderer.material = originalMaterial;
    }
}
