using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0; 

    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    public float collectables = 0;

    private Component[] audios;
    private AudioSource bruh;
    private AudioSource nice;
    private AudioSource amogus;
    private AudioSource wiisports;
    private VideoPlayer nggyu; //Never Gonna Give You Up
    public Material nggyu_material; //Rick Ball

    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audios = GetComponents(typeof(AudioSource));
        foreach(AudioSource audio in audios)
        {
            string name = audio.clip.ToString().Replace(" (UnityEngine.AudioClip)", "");
            if (name == "bruh")
            {
                bruh = audio;
            }else if(name == "nice")
            {
                nice = audio;
            }else if(name == "amogus")
            {
                amogus = audio;
            }else if(name == "wiisports")
            {
                wiisports = audio;
            }
        }

        if(bruh == null) //Makes sure the audio exists
        {
            Debug.LogError("bruh not found!");
        }
        if(nice == null)
        {
            Debug.LogError("nice not found!");
        }
        if(amogus == null)
        {
            Debug.LogError("amogus not found!");
        }
        if(wiisports == null)
        {
            Debug.LogError("wiisports not found!");
        }

        count = 0;
        SetCountText();
        winTextObject.gameObject.SetActive(false);

        //Get the VideoPlayer component
        nggyu = GameObject.Find("Ground").GetComponent<VideoPlayer>();
        if (nggyu == null) //Makes sure the video exists
        {
            Debug.LogError("nggyu not found!");
        }


        //Generates the pickups, -1 because of Pacer
        for (var i = 0; i < collectables - 1; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-9.5f, 9.5f), 1, Random.Range(-9.5f, 9.5f)), Quaternion.identity);
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= collectables)
        {
            winTextObject.SetActive(true);
            wiisports.Stop();
            nggyu.Play(); //Plays Never Gonna Give You Up
            //Switches the ball's material to the rick ball
            gameObject.GetComponent<MeshRenderer>().material = nggyu_material;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            int randomChance = Random.Range(1, 100);
            if (randomChance == 42)
            {
                amogus.Play();
            }
            else if (count + 1 == 69) //plays the nice sound effect if count is 69
            {
                nice.Play();
            }
            else
            {
                bruh.Play();
            }
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        

    }
}
