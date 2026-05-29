using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class Tutorial : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public TextMeshProUGUI text;

    public float fadeSpeed = 2f;
    public float displayTime = 2f;

    private Queue<string> messageQueue = new Queue<string>();
    private bool isShowing = false;

    public void ShowMessage(string message)
    {
        messageQueue.Enqueue(message);

        if (!isShowing)
        {
            StartCoroutine(ProcessQueue());
        }
    }

    IEnumerator ProcessQueue()
    {
        isShowing = true;

        while (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();
            text.text = message;

            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.deltaTime * fadeSpeed;
                yield return null;
            }

            yield return new WaitForSeconds(displayTime);

            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.deltaTime * fadeSpeed;
                yield return null;
            }
        }

        isShowing = false;
    }
}

