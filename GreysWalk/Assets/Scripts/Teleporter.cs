using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Teleporter : MonoBehaviour
{
    public Transform targetLocation;
    public Image fadeImage;
    public float fadeSpeed = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportWithFade(other.transform, targetLocation.position);
        }
    }

    public void TeleportWithFade(Transform player, Vector3 targetPosition)
    {
        StartCoroutine(FadeSequence(player, targetPosition));
    }

    private IEnumerator FadeSequence(Transform player, Vector3 targetPosition)
    {
        // 1. Einblenden (Schwarz werden)
        yield return StartCoroutine(Fade(1));

        // 2. Eigentliche Teleportation
        // WICHTIG: Wenn du einen CharacterController nutzt, setze ihn kurz 'enabled = false'
        player.position = targetPosition;
        
        // Kurze Pause im Schwarz (für das "Laden"-Gefühl wie in GTA)
        yield return new WaitForSeconds(0.5f);

        // 3. Ausblenden (Wieder sichtbar werden)
        yield return StartCoroutine(Fade(0));
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float currentAlpha = fadeImage.color.a;
        float time = 0;

        while (time < 1)
        {
            time += Time.deltaTime * fadeSpeed;
            float newAlpha = Mathf.Lerp(currentAlpha, targetAlpha, time);
            fadeImage.color = new Color(0, 0, 0, newAlpha);
            yield return null;
        }
    }
}