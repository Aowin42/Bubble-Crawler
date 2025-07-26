using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Controlling main behaviour of camera, but most specifics are done by the Virtual Camera
    /// </summary>
    [SerializeField]
    private float CamSpeed;


    private GameObject player;
    private Transform playerPos;
    private Camera MainCam;


    // Boundaries for the camera
    public float camHeight;
    public float camWidth;
    public float left;
    public float right;
    public float bottom;
    public float top;




    // Start is called before the first frame update
    void Start()
    {
        MainCam = GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerPos = player.transform;

        camHeight = 2f * MainCam.orthographicSize;
        camWidth = camHeight * MainCam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps the z component of the camera
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);

        // Caluculating a bounding box for the camera
        left = MainCam.transform.position.x - camWidth / 2f;
        right = MainCam.transform.position.x + camWidth / 2f;
        bottom = MainCam.transform.position.y - camHeight / 2f;
        top = MainCam.transform.position.y + camHeight / 2f;
    }
}
