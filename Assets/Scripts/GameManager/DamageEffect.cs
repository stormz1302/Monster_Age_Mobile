using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageEffect : MonoBehaviour
{
    public Image damageImage;
    public float fadeDuration = 0.3f;

    public IEnumerator ShowDamageFlash()
    {
       
        damageImage.color = new Color(1f, 0f, 0f, 0.3f);

        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0.5f, 0f, elapsedTime / fadeDuration);
            damageImage.color = new Color(1f, 0f, 0f, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        
        damageImage.color = new Color(1f, 0f, 0f, 0f);
    }
}
