using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour
{  
    [SerializeField]
    private float duration = 0.4f;

    private bool faded;
    private Vector2 storedPosition;
    private Vector2 hidePosition = new Vector2(0, 500);
    private Vector2 visableStartPosition = new Vector2(203, 204);

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Fade();
        }
    }

    public void Fade()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        // Toggle the end value depending on the faded state
        StartCoroutine(DoFade(canvasGroup, canvasGroup.alpha, faded ? 1 : 0));

        // Toggle the faded state
        faded = !faded;
    }

    public IEnumerator DoFade( CanvasGroup canvasGroup, float start, float end)
    {
        Debug.Log(transform.position);
        float counter = -1.0f;

        while (counter < duration)
        {  
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / duration);
            counter += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = Mathf.RoundToInt(canvasGroup.alpha);

        if (canvasGroup.alpha > 0)
        {
            transform.position = visableStartPosition;
            Debug.Log("Panel Is Visable");
        }
        if (canvasGroup.alpha == 0)
        {
            transform.position = hidePosition;
            Debug.Log("Panel Is NOT Visable");
        }

    }

}
