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

    public GameObject door2;

    int[] corectPath = { 1, 2, 3, 4 };
    int current;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
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
        yield return new WaitForSeconds(5);
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
                Vector3 now = door2.transform.position;
                door2.transform.position = new Vector3(now.x, now.y + 50, now.z);
            }
            else if (current == 4)
            {
                meshRenderer4.material.color = Color.green;
            }
            else StartCoroutine(ClearPath());


        }
    }
}