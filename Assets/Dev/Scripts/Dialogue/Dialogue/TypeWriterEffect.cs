using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float writeSpd = 50f;
    public bool isRunning { get; private set; }
    private Coroutine typingCoroutine;
    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }
    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        isRunning = false;
    }
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        isRunning = true;
        textLabel.text = string.Empty;
        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)
        {
            t += Time.deltaTime * writeSpd;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(value: charIndex, min: 0, max: textToType.Length);

            textLabel.text = textToType.Substring(startIndex: 0, length: charIndex);

            yield return null;
        }
        isRunning = false;
        //textLabel.text = textToType;
    }
    
}
