using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessageController : MonoBehaviour
{
    private TextMeshProUGUI _messageText;

    private void Awake()
    {
        _messageText = FindObjectOfType<Canvas>().transform.Find("MessageText").GetComponent<TextMeshProUGUI>();
    }

    public void DrawMessage(string text, float delayTime = 0.1f)
    {
        StartCoroutine(DrawMessageCoroutine(text, delayTime));
    }
    
    public void DrawMessage(string text)
    {
        StartCoroutine(DrawMessageCoroutine(text));
    }
    public void DrawMessage(string text, Action action, float delayTime = 0.1f)
    {
        StartCoroutine(DrawMessageCoroutine(text, action, delayTime));
    }
    
    private IEnumerator DrawMessageCoroutine(string text, float delayTime = 0.1f)
    {
        string message = "";
        for (int i = 0; i < text.Length; i++)
        {
            message += text[i];
            _messageText.text = message;
            yield return new WaitForSeconds(delayTime);
        }
    }
    
    private IEnumerator DrawMessageCoroutine(string text, Action action, float delayTime)
    {
        string message = "";
        for (int i = 0; i < text.Length; i++)
        {
            message += text[i];
            _messageText.text = message;
            yield return new WaitForSeconds(delayTime);
        }

        yield return new WaitForSeconds(1);
        action?.Invoke();
    }
}
