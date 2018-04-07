using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Directions
{
    public bool Up = false;
    public bool Down = false;
    public bool Left = false;
    public bool Right = false;

    public Transform upPosition;
    public Transform downPosition;
    public Transform leftPosition;
    public Transform rightPosition;
}

public class CameraController : MonoBehaviour {

    public Directions directions;
    public AnimationCurve curve;
    IEnumerator moveCamera;
    GameObject mainCamera;
    float cameraMoveDuration = 0.3f;

    void Start () {
        mainCamera = Camera.main.gameObject;
	}
	
	void Update () {
		
	}

    void StartMovingCamera(Transform pos)
    {
        if (moveCamera != null)
        {
            StopCoroutine(moveCamera);
            moveCamera = null;
        }
        moveCamera = MoveCamera(pos);
        StartCoroutine(moveCamera);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Vector3 move = col.gameObject.GetComponent<Rigidbody2D>().velocity;
            if (move.y > 0 && directions.Up)
            {
                StartMovingCamera(directions.upPosition);
            }
            if (move.y < 0 && directions.Down)
            {
                StartMovingCamera(directions.downPosition);
            }
            if (move.x < 0 && directions.Left)
            {
                StartMovingCamera(directions.leftPosition);
            }
            if (move.x > 0 && directions.Right)
            {
                StartMovingCamera(directions.rightPosition);
            }
        }
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
