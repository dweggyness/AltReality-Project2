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
    int currentCode = 0;
    public MeshRenderer checkerRoom3;
    public GameObject textField;

    public GameObject door1;
    public GameObject door2;

    private Vector3 doorStartPos1;
    public Vector3 doorEndPos1;
    private Vector3 doorStartPos2;
    public Vector3 doorEndPos2;
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
        doorStartPos1 = door1.transform.position;
        doorStartPos2 = door2.transform.position;
        StartCoroutine(ClearPath());
        checkerRoom3.material.color = Color.black;
        
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
                StartCoroutine(OpenDoor(door1, doorStartPos1, doorEndPos1));
            }
            else if (current == 4)
            {
                meshRenderer4.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());


        }
    }
    IEnumerator OpenDoor(GameObject doorObject, Vector3 doorStartPos, Vector3 doorEndPos)
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
                doorObject.transform.position = Vector3.MoveTowards(doorStartPos, doorEndPos, openDoorElapsedTime*3 / openDoorDuration);
                openDoorElapsedTime += Time.deltaTime;
                Debug.Log(doorObject.transform.position);
                Debug.Log(openDoorElapsedTime);
            }
            else if (openDoorElapsedTime > openDoorDuration)
            {
                StopCoroutine(OpenDoor(doorObject, doorStartPos, doorEndPos));
            }
            yield return null;
        }

    }

    public void OnClick(int t)
    {
        string resnow = "";
        
        userCode[currentCode] = t;
        currentCode++;

        //writing the code written by the user into the field
        for (int i = 0; i < currentCode; i++)
        {
            resnow = resnow + userCode[i].ToString();
        }
        textField.GetComponent<UnityEngine.UI.Text>().text = resnow;
        if (currentCode == 3)
        {
            if (CheckTwoArrays(userCode, correctCode))
            {
                StartCoroutine(checkCode(Color.green));
                OpenDoor(door2, doorStartPos2, doorEndPos2);
            } else
            {
                StartCoroutine(checkCode(Color.red));
                currentCode = 0;
                userCode = new int[3];
                
            }
            
        }
    }
    bool CheckTwoArrays(int[] ar1, int[] ar2)
    {
        for (int i = 0; i < 3; i++)
        {
            if (ar1[i] != ar2[i]) return false;
        }
        return true;
    }
    IEnumerator checkCode(Color clr)
    {
        checkerRoom3.material.color = clr;
        yield return new WaitForSeconds(2);
        textField.GetComponent<UnityEngine.UI.Text>().text = "";
        checkerRoom3.material.color = Color.black;
    }
}