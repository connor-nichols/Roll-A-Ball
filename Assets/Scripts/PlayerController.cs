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

    private AudioSource bruh;
    private VideoPlayer nggyu;
    public Material nggyu_material;

    public GameObject prefab;

    private static readonly System.Random getrandom = new System.Random();


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        bruh = GetComponent<AudioSource>();
        if(bruh == null)
        {
            Debug.LogError("bruh not found!");
        }

        count = 0;
        SetCountText();
        winTextObject.gameObject.SetActive(false);

        nggyu = GameObject.Find("Ground").GetComponent<VideoPlayer>();
        if (nggyu == null)
        {
            Debug.LogError("nggyu not found!");
        }

        
        for (var i = 0; i < collectables; i++)
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
            nggyu.Play();
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
            bruh.Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
        }
        

    }
}
