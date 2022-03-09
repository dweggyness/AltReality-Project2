using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class Player : MonoBehaviour
{

    public MeshRenderer meshRenderer1;
    public MeshRenderer meshRenderer2;
    public MeshRenderer meshRenderer3;
    public MeshRenderer meshRenderer4;


    int[] corectPath = { 1, 2, 3, 4 };
    int current;
    int[] correctCode = { 7, 6, 4 };
    int[] userCode = new int[3];
    int currentCode;

    public GameObject door;

    private Vector3 doorStartPos;
    public Vector3 doorEndPos;
    //public AudioSource beepSound;
    //public AudioSource doorSound;

    public float openDoorElapsedTime = 0.0f;
    public float openDoorDuration = 4.0f;
    float lerpValue;
    bool isFirstRun = true;


    // Start is called before the first frame update
    void Start()
    {
        current = 0;
        doorStartPos = door.transform.position;
        StartCoroutine(ClearPath());
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ClearPath()
    {
        meshRenderer1.material.color = Color.red;
        meshRenderer2.material.color = Color.red;
        meshRenderer3.material.color = Color.red;
        meshRenderer4.material.color = Color.red;
        yield return new WaitForSeconds(2);
        meshRenderer1.material.color = Color.gray;
        meshRenderer2.material.color = Color.gray;
        meshRenderer3.material.color = Color.gray;
        meshRenderer4.material.color = Color.gray;
        current = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("press1"))
        {
            meshRenderer1.material.color = Color.cyan;
            if (corectPath[current] == 1)
            {
                meshRenderer1.material.color = Color.green;
                current = 1;
            }
            else if(current == 1)
            {
                meshRenderer1.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());

        }
        else if (other.gameObject.CompareTag("press2"))
        {
            if (corectPath[current] == 2)
            {
                meshRenderer2.material.color = Color.green;
                current = 2;
            }
            else if (current == 2)
            {
                meshRenderer2.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());

        }
        else if (other.gameObject.CompareTag("press3"))
        {
            if (corectPath[current] == 3)
            {
                meshRenderer3.material.color = Color.green;
                current = 3;
            }
            else if (current == 3)
            {
                meshRenderer3.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());

        }
        else if (other.gameObject.CompareTag("press4"))
        {
            if (corectPath[current] == 4)
            {
                meshRenderer4.material.color = Color.green;
                current = 4;
                StartCoroutine(OpenDoor());
            }
            else if (current == 4)
            {
                meshRenderer4.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());


        }
    }
    IEnumerator OpenDoor()
    {
        if (isFirstRun)
        { // delays door opening by 2 seconds
            yield return new WaitForSeconds(2);
            isFirstRun = false;
        }

        //doorSound.Play();
        while (true)
        {
            if (openDoorElapsedTime < openDoorDuration)
            {
                door.transform.position = Vector3.MoveTowards(doorStartPos, doorEndPos, openDoorElapsedTime*3 / openDoorDuration);
                openDoorElapsedTime += Time.deltaTime;
                Debug.Log(door.transform.position);
                Debug.Log(openDoorElapsedTime);
            }
            else if (openDoorElapsedTime > openDoorDuration)
            {
                StopCoroutine(OpenDoor());
            }
            yield return null;
        }

    }

    public void OnClick(float t)
    {
        if (userCode[2] != null)
        {

        }
    }
}