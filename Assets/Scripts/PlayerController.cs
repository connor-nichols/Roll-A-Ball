using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
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

    private GameObject ground;

    private Component[] audios; //Holds all of the audios before they're assigned
    private AudioSource bruh;
    private AudioSource nice;
    private AudioSource amogus;
    private AudioSource wiisports;
    private VideoPlayer never_gonna; //Never Gonna Give You Up
    public Material nggyu_material; //Rick Ball

    public GameObject prefab;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        audios = GetComponents(typeof(AudioSource));
        foreach(AudioSource audio in audios) //Sets each audio to it's variable
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

        ground = GameObject.Find("Ground"); //also used later to set ground color, we set it here since we also need it to get the video variable working
        never_gonna = ground.GetComponent<VideoPlayer>();
        if (never_gonna == null) //Makes sure the video exists
        {
            Debug.LogError("never_gonna not found!");
        }


        //Generates the pickups, -1 because of Pacer
        for (var i = 0; i < collectables - 1; i++)
        {
            //TODO: make parents work
            Instantiate(prefab, new Vector3(Random.Range(-9.5f, 9.5f), 1, Random.Range(-9.5f, 9.5f)), Quaternion.identity, GameObject.FindGameObjectWithTag("PlayArea").transform);
        }
    }
    /*
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }
    */
    
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= collectables)
        {
            //Gets the ground renderer
            var groundRender = ground.GetComponent<Renderer>();

            winTextObject.SetActive(true);

            wiisports.Stop(); //Stops the Wii Sports background music
            never_gonna.Play(); //Plays Never Gonna Give You Up

            //Switches the ground color to white to avoid putting a red filter over the video
            groundRender.material.SetColor("_Color", Color.white);

            //Switches the ball's material to the rick ball
            gameObject.GetComponent<MeshRenderer>().material = nggyu_material;
        }
    }

    /*
    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            int randomChance = Random.Range(1, 100);
            if (randomChance == 42) //1/100 chance of the amogus audio playing instead
            {
                amogus.Play();
            }
            else if (count + 1 == 69) //plays the nice sound effect if count is 69
            {
                nice.Play();
            }
            else // Plays the default bruh if nothing else is found.
            {
                bruh.Play();
            }
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        

    }
}
