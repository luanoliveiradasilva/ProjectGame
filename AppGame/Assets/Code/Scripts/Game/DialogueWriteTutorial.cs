using UnityEngine;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;
using System.Linq;


public class DialogueWriteTutorial : MonoBehaviour
{

    public static DialogueWriteTutorial instance;
    public float writingSpeed = 15;
    public TMPAnimated sourceText;
    public RectTransform dialogueContainer;
    public float firsMessageDelay = 0.5f;
    public float animDuration = 0.5f;
    public Ease animEase = Ease.InOutBack;
    public float delayToSkip = 0.5f;
    public float delayToUnfocusCamera = 1f;


    List<string> sentencesToRead;
    bool isReading, sentenceFinished;
    int sentenceIndex;
    Vector3 defaultScale;

    private void Awake()
    {
        instance = this;

        sourceText.speed = writingSpeed;
        /* defaultScale = dialogueContainer.localScale;
        dialogueContainer.localScale = Vector3.zero; */
    }

    public void ReadSentences(string[] sentences)
    {
        sentencesToRead = sentences.ToList();
        sentenceIndex = -1;


        if (sentenceIndex == 0)
            sourceText.ReadText(sentencesToRead[sentenceIndex], firsMessageDelay);
        else
            sourceText.ReadText(sentencesToRead[sentenceIndex]);
    }

    void ShowDialogue() => dialogueContainer.DOScale(defaultScale, animDuration).SetEase(animEase);

    void HideDialogue() => dialogueContainer.DOScale(Vector3.zero, animDuration).SetEase(animEase);

}
