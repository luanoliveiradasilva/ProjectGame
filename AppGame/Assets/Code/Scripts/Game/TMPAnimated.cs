using System.Collections;
using TMPro;
using UnityEngine;
using static UEventHandler;

namespace TMPro
{
    public class TMPAnimated : TextMeshProUGUI
    {
        public float speed = 10;

        public UEvent<string> OnDialogueAction = new UEvent<string>();
        public UEvent<string> OnDialogueEmotion = new UEvent<string>();
        public UEvent OnDialogueFinished = new UEvent();

        private Coroutine readingCoroutine;

        public void StopText()
        {
            if (readingCoroutine != null)
                StopCoroutine(readingCoroutine);

            readingCoroutine = null;
        }
        public void ReadText(string newText, float delay = 0)
        {
            text = string.Empty;
            // split the whole text into parts based off the <> tags 
            // even numbers in the array are text, odd numbers are tags
            string[] subTexts = newText.Split('<', '>');

            // textmeshpro still needs to parse its built-in tags, so we only include noncustom tags
            string displayText = "";
            for (int i = 0; i < subTexts.Length; i++)
            {
                if (i % 2 == 0)
                    displayText += subTexts[i];
                else if (!isCustomTag(subTexts[i].Replace(" ", "")))
                    displayText += $"<{subTexts[i]}>";
            }
            // check to see if a tag is our own
            bool isCustomTag(string tag)
            {
                return tag.StartsWith("speed=") || tag.StartsWith("pause=") || tag.StartsWith("emotion=") || tag.StartsWith("action");
            }

            // send that string to textmeshpro and hide all of it, then start reading
            text = displayText;
            maxVisibleCharacters = 0;
            StartCoroutine(Read(delay));

            IEnumerator Read(float delay)
            {
                yield return new WaitForSeconds(delay);

                int subCounter = 0;
                int visibleCounter = 0;
                while (subCounter < subTexts.Length)
                {
                    // if 
                    if (subCounter % 2 == 1)
                    {
                        yield return EvaluateTag(subTexts[subCounter].Replace(" ", ""));
                    }
                    else
                    {
                        while (visibleCounter < subTexts[subCounter].Length)
                        {
                            //onTextReveal.Invoke(subTexts[subCounter][visibleCounter]);
                            visibleCounter++;
                            maxVisibleCharacters++;
                            yield return new WaitForSeconds(1f / (2f * speed));
                        }
                        visibleCounter = 0;
                    }
                    subCounter++;
                }
                yield return null;

                WaitForSeconds EvaluateTag(string tag)
                {
                    if (tag.Length > 0)
                    {
                        var subtag = tag.Split('=')[1];
                        if (tag.StartsWith("speed="))
                        {
                            speed = float.Parse(subtag);
                        }
                        else if (tag.StartsWith("pause="))
                        {
                            return new WaitForSeconds(float.Parse(subtag));
                        }
                        else if (tag.StartsWith("emotion="))
                        {
                            OnDialogueEmotion.TryInvoke(subtag);
                        }
                        else if (tag.StartsWith("action="))
                        {
                            OnDialogueAction.TryInvoke(subtag);
                        }
                    }
                    return null;
                }
                OnDialogueFinished.TryInvoke();
            }
        }
    }
}
