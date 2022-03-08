using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollision1 : MonoBehaviour
{
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hz");
            //MeshRenderer another = other.gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material.color = Color.cyan;
        }
    }
}
