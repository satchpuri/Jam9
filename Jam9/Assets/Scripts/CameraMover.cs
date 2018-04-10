using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour {
    public GameObject player1;
    public GameObject player2;
    public Transform[] spawnPositions;
    public Transform[] cameraPos;
    GameObject mainCamera;
    float cameraMoveDuration = 0.3f;
    [SerializeField] AnimationCurve curve;
    // Use this for initialization
    private void Start()
    {
        mainCamera = Camera.main.gameObject;
    }

    public void MoveCamera(int level)
    {
        StartCoroutine(MoveCamera(cameraPos[level]));
        player1.transform.position = spawnPositions[level].position;
        player2.transform.position = spawnPositions[level].position;
    }
    IEnumerator MoveCamera(Transform pos)
    {
        Vector3 start = mainCamera.transform.position;
        Vector3 destination = new Vector3(pos.position.x, pos.position.y, mainCamera.transform.position.z);
        for (float i = 0f; i < cameraMoveDuration; i += Time.deltaTime)
        {
            Vector3 newPos = Vector3.Lerp(start, destination, curve.Evaluate(i / cameraMoveDuration));
            mainCamera.transform.position = newPos;
            yield return 0;
        }
        mainCamera.transform.position = destination;
    }
}
