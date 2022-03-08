using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReaderController : MonoBehaviour
{
    public GameObject readerScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Card"))
        {
            Material readerMaterial = readerScreen.GetComponent<Renderer>().material;

            readerMaterial.color = new Color(33, 33, 33);
        }
    }
}