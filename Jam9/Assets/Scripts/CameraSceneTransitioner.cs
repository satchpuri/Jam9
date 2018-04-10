using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneTransitioner : MonoBehaviour {
    public static CameraSceneTransitioner sharedInstance;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] Transform[] cameraPoints;
    [SerializeField] AnimationCurve curve;
    GameObject myCamera;

    float cameraMoveDuration = 0.3f;

    void Start () {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        myCamera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		//if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Transition(1);
        //}
	}

    public void Transition(int levelNumber)
    {
        player1.transform.position = spawnPoints[levelNumber-1].position;
        player2.transform.position = spawnPoints[levelNumber-1].position;

        StartCoroutine(MoveCamera(cameraPoints[levelNumber]));
    }

    IEnumerator MoveCamera(Transform pos)
    {
        yield return 0;
        Vector3 start = myCamera.transform.position;
        Vector3 destination = pos.position;
        for (float i = 0f; i < cameraMoveDuration; i += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(start, destination, curve.Evaluate(i / cameraMoveDuration));
            myCamera.transform.position = newPos;
            yield return 0;
        }
        myCamera.transform.position = destination;
    }
}
