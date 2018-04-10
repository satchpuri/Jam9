using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour {
    public const KeyCode DialogueButton = KeyCode.Space;
    public static DialogueController sharedInstance;
    [SerializeField] AnimationCurve speechIndicatorEnterCurve;
    [SerializeField] AnimationCurve speechIndicatorExitCurve;
    [SerializeField] GameObject speechIndicatorPrefab;
    [SerializeField] GameObject speechBubblePrefab;
    [System.NonSerialized] public bool dialogueActive;

    float showSpeechIndicatorTime = 0.25f;
    float hideSpeechIndicatorTime = 0.1f;

    IEnumerator routine;
    GameObject speechIndicator;
    SpeechBubble speechBubble;
	void Start () {
		if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        dialogueActive = false;
        speechIndicator = Instantiate(speechIndicatorPrefab) as GameObject;
        speechBubble = (Instantiate(speechBubblePrefab) as GameObject).GetComponent<SpeechBubble>();
        speechIndicator.SetActive(false);
        speechBubble.gameObject.SetActive(false);
	}

    void StartRoutine(IEnumerator r)
    {
        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }
        routine = r;
        StartCoroutine(routine);
    }

    IEnumerator IShowSpeechIndicator(Vector3 npcPosition, AnimationCurve curve, float time)
    {
        speechIndicator.transform.position = npcPosition + new Vector3(0.3f, 0.2f);
        speechIndicator.SetActive(true);

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            speechIndicator.transform.localScale = curve.Evaluate(i / time) * Vector3.one;
            yield return 0;
        }
        speechIndicator.transform.localScale = curve.Evaluate(1f) * Vector3.one;
    }

    public void ShowSpeechIndicator(Vector3 npcPosition)
    {
        StartRoutine(IShowSpeechIndicator(npcPosition, speechIndicatorEnterCurve, showSpeechIndicatorTime));
    }

    public void HideSpeechIndicator(Vector3 npcPosition)
    {
        StartRoutine(IShowSpeechIndicator(npcPosition, speechIndicatorExitCurve, hideSpeechIndicatorTime));
    }

    public void ShowDialogue(string[] text, Vector3 npcPosition)
    {
        StartRoutine(IShowDialogue(text, npcPosition));
    }

    IEnumerator WaitForSkip()
    {
        while (!Input.GetKeyDown(DialogueButton))
        {
            yield return 0;
        }
    }

    IEnumerator IShowDialogue(string[] text, Vector3 npcPosition)
    {
        dialogueActive = true;

        yield return IShowSpeechIndicator(npcPosition, speechIndicatorExitCurve, hideSpeechIndicatorTime);
        foreach (string str_ in text)
        {
            string str = str_.Replace("\\n", "\n");
            speechBubble.gameObject.SetActive(true);
            yield return speechBubble.Move(npcPosition, showSpeechIndicatorTime, str, speechIndicatorEnterCurve);
            yield return speechBubble.FillText(str);
            //speechBubble.text.text = text;

            yield return 0;
            yield return WaitForSkip();

            speechBubble.text.text = "";
            yield return speechBubble.Move(npcPosition, hideSpeechIndicatorTime, str, speechIndicatorExitCurve);


        }
        dialogueActive = false;
        yield return IShowSpeechIndicator(npcPosition, speechIndicatorEnterCurve, showSpeechIndicatorTime);
    }
	
	void Update () {
		
	}
}
