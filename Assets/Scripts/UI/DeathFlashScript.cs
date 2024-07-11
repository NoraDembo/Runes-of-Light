using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFlashScript : MonoBehaviour
{
    CanvasGroup flashImage;
    public float reductionRate;
    // Start is called before the first frame update
    void Start()
    {
        flashImage = GetComponent<CanvasGroup>();
        flashImage.alpha = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(flashImage.alpha > 0)
        {
            flashImage.alpha -= reductionRate * Time.deltaTime;
        }
    }

    public void Flash()
    {
        flashImage.alpha = 0.5f;
        GetComponent<AudioSource>().Play();
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
    }
}
