using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour {
    [SerializeField] GameObject center;
    [SerializeField] GameObject right;
    [SerializeField] GameObject bubble;
    public TextMesh text;
    //[SerializeField] AnimationCurve moveInCurve;
    //[SerializeField] AnimationCurve moveOutCurve;
    const float horizontalOffset = 0.51f;

	// Use this for initialization
	void Start () {
	}

    //Set the text mesh's text,
    //Get the bounds,
    //Then set the text back to ""
    public void ScaleWindow(string newText)
    {
        text.text = newText;
        center.transform.localScale = new Vector3(GetTextExtents().x * 4f, 1f, 1f);
        right.transform.localPosition = new Vector3((center.transform.localScale.x - 1) * horizontalOffset, 0f, 0f);

        text.text = "";
    }
    // Update is called once per frame
    void Update () {
		
	}

    public IEnumerator Move(Vector3 npcPosition, float time, string newText, AnimationCurve curve)
    {
        ScaleWindow(newText);
        transform.position = npcPosition + new Vector3(0.3f, 0.2f);

        for (float i = 0f; i < time; i += Time.deltaTime)
        {
            bubble.transform.localScale = curve.Evaluate(i / time) * Vector3.one;
            yield return 0;
        }
        bubble.transform.localScale = curve.Evaluate(1f) * Vector3.one;
    }

    Vector3 GetTextExtents()
    {
        return text.GetComponent<Renderer>().bounds.extents;
    }
}
