using UnityEngine;
using UnityEngine.UI;

public class FadePanelUI : MonoBehaviour
{
    [SerializeField] private Image fadeOutImage;
    [Range(0.1f, 10f), SerializeField] private float fadeOutSpeed = 5f;
    [Range(0.1f, 10f), SerializeField] private float fadeInSpeed = 5f;
    
    [SerializeField] private Color fadeOutStartColor;
    
    public bool isFadingOut { get; private set; }
    public bool isFadingIn { get; private set; }
    
    private void Awake()
    {
        fadeOutStartColor.a = 0;
    }

    private void Update()
    {
        if (isFadingOut)
        {
            if (fadeOutImage.color.a < 1)
            {
                fadeOutStartColor.a += Time.deltaTime * fadeOutSpeed;
                fadeOutImage.color = fadeOutStartColor;
            }
            else
            {
                isFadingOut = false;
            }
        }

        if (isFadingIn)
        {
            if (fadeOutImage.color.a >= 0)
            {
                fadeOutStartColor.a -= Time.deltaTime * fadeInSpeed;
                fadeOutImage.color = fadeOutStartColor;
            }
            else
            {
                isFadingIn = false;
            }
        }
    }

    // Event listener
    public void StartFadeOut()
    {
        fadeOutImage.color = fadeOutStartColor;
        isFadingOut = true;
    }

    // Event listener
    public void StartFadeIn()
    {
        if (!(fadeOutImage.color.a >= 1)) return;
        
        fadeOutImage.color = fadeOutStartColor;
        isFadingIn = true;
    }
}