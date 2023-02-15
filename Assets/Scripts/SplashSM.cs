using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using TMPro;

public class SplashSM : MonoBehaviour
{
    public TMP_Text start;
    public TMP_Text controls;
    public TMP_Text toGame;
    private AudioSource relax;
    // Start is called before the first frame update
    void Start()
    {
        relax = GetComponent<AudioSource>();
        start.text = "Astroids";
        controls.text = "Controls: WASD or Arrow Keys for movement";
        toGame.text = "Click the button to start!";
    }

    // Update is called once per frame
    void Update()
    {
        if (!relax.isPlaying)
            relax.PlayOneShot(relax.clip, 0.5f);
        if (Input.GetKey(KeyCode.Space))
            UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

}
