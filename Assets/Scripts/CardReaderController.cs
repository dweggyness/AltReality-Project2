using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderController : MonoBehaviour
{
    public GameObject readerScreen;
    public GameObject door;

    public Vector3 doorStartPos;
    public Vector3 doorEndPos = new Vector3(2.613f,-0.111f,4.212f);
    public AudioSource beepSound;
    public AudioSource doorSound;

    float openDoorElapsedTime = 0.0f;
    float openDoorDuration = 4.0f;
    float lerpValue;
    bool isFirstRun = true;

    private void Start() {
      doorStartPos = door.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Card"))
        {
            readerScreen.GetComponent<MeshRenderer>().material.color = new Color(0.2f, 1f, 0.2f);
            
            beepSound.Play();
            StartCoroutine(OpenDoor());
        }
    }

    IEnumerator OpenDoor()
    {
        if (isFirstRun) { // delays door opening by 2 seconds
          yield return new WaitForSeconds(2);
          isFirstRun = false;
        }

        doorSound.Play();
        while (true)
        {
            if (openDoorElapsedTime < openDoorDuration)
            {
                door.transform.position = Vector3.MoveTowards(doorStartPos, doorEndPos, openDoorElapsedTime / openDoorDuration);
                openDoorElapsedTime += Time.deltaTime;
            } else if (openDoorElapsedTime > openDoorDuration) {
              StopCoroutine(OpenDoor());
            }
            yield return null;
        }
    }

}