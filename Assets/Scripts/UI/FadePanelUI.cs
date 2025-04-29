using System.Collections;
using UnityEngine;

public class FadePanelUI : MonoBehaviour
{
    public float fadeDuration = 1.0f;
    public float inputEnableTime = 0.5f;
    
    private CanvasGroup _canvasGroup;
    private Coroutine _fadeCoroutine;
    private bool _isInputEnable = false;

    private void Awake()
    {
        // Get the CanvasGroup component attached to this GameObject.
        _canvasGroup = GetComponent<CanvasGroup>();
        // Ensure the panel is fully visible initially if it's enabled in the editor.
        _canvasGroup.alpha = 1.0f;
        // Reset flag
        _isInputEnable = false;

    }
    
    void OnEnable()
    {
        // Start the fade-out coroutine when the GameObject is enabled.
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
        }
        _fadeCoroutine = StartCoroutine(FadeOut());
        GameManager.Instance.isOpenUI = true;
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = _canvasGroup.alpha;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float clampedInputEnableTime = Mathf.Clamp(inputEnableTime, 0f, fadeDuration);
            
            if (!_isInputEnable && timer >= clampedInputEnableTime)
            {
                GameManager.Instance.isOpenUI = false;
                _isInputEnable = true;
            }
            
            float newAlpha = Mathf.Lerp(startAlpha, 0f, timer / fadeDuration);
            _canvasGroup.alpha = newAlpha;
            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is exactly 0 at the end.
        _canvasGroup.alpha = 0f;
        
        // This prevents interaction and rendering of the panel when it's invisible.
        gameObject.SetActive(false);
        // canvasGroup.interactable = false;
        // canvasGroup.blocksRaycasts = false;

        _fadeCoroutine = null; // Clear the coroutine reference
    }

    void OnDisable()
    {
        // Stop the coroutine if the GameObject is disabled while fading.
        if (_fadeCoroutine != null)
        {
            StopCoroutine(_fadeCoroutine);
            _fadeCoroutine = null;
        }
        // Reset alpha to full when disabled, so it's ready for the next enable.
        if (_canvasGroup != null)
        {
            _canvasGroup.alpha = 1.0f;
        }

        _isInputEnable = false;
    }
}