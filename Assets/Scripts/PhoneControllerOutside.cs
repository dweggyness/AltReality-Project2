using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneControllerOutside : MonoBehaviour
{

    bool lightActivated = false;
    bool phoneRung = false;
    public AudioSource phoneAudio;
    public AudioSource phoneRingingAudio;
    public float delay = 13.0f;

    private void Awake()
    {
        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }

    public void updatePulledAmount(float t)
    {
        if (t > 0.99 && !lightActivated)
        {
            startPhoneCall();
        };
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerBox"))
        {
            if (!phoneRung)
            {
                ringPhone();
                phoneRung = true;
            }
        }
    }

    private void startPhoneCall()
    {
        lightActivated = true;
        this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        phoneRingingAudio.Stop();
        phoneAudio.Play();

        StartCoroutine(turnLightOffAfterDelay());
    }

    public void ringPhone()
    {
        phoneRingingAudio.Play();
    }

    IEnumerator turnLightOffAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}
