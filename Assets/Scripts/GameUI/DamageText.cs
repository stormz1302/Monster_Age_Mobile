using TMPro;
using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour
{
    public float moveUpDistance = 1f; 
    public float duration = 1f; 
    public float initialScale = 1.5f; 
    public float finalScale = 1f; 

    public TextMesh text;

    private void Awake()
    {
        text = GetComponent<TextMesh>();
    }

    public void Initialize(int damage)
    {
        text.text = damage.ToString(); 
        transform.localScale = Vector3.one * initialScale; 
        StartCoroutine(ShowDamageEffect());
    }

    private IEnumerator ShowDamageEffect()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + Vector3.up * moveUpDistance;
        float elapsedTime = 0f;

        
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);

            Color color = text.color;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            text.color = color;

            float scale = Mathf.Lerp(initialScale, finalScale, elapsedTime / duration);
            transform.localScale = Vector3.one * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
