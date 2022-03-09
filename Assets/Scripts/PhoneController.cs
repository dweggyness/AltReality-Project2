using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneController : MonoBehaviour
{

    bool lightActivated = false;
    public AudioSource phoneAudio;
    public AudioSource phoneRingingAudio;
    public float delay = 27.0f;

    private void Awake()
    {
        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        // StartCoroutine(playPhoneRingingSound());
    }

    public void updatePulledAmount(float t)
    {
        if (t > 0.99 && !lightActivated)
        {
            startPhoneCall();
        };
    }

    private void startPhoneCall()
    {
        lightActivated = true;
        this.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
        phoneRingingAudio.Stop();
        phoneAudio.Play();

        StopCoroutine(playPhoneRingingSound());
        StartCoroutine(turnLightOffAfterDelay());
    }

    IEnumerator playPhoneRingingSound()
    {
        yield return new WaitForSeconds(5); // phone starts ringing after 5 secs
        phoneRingingAudio.Play();
    }

    IEnumerator turnLightOffAfterDelay()
    {
        yield return new WaitForSeconds(delay);

        this.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
    }
}
