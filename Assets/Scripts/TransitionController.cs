using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    /// <summary>
    /// Unfinished Script. Originally though of as the controller used for going between arenas
    /// </summary>


    [SerializeField]
    private GameObject BubbleTransporter;





    // Start is called before the first frame update
    void Start()
    {
        
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.name);    
    }




    // Update is called once per frame
    void Update()
    {

    }
}
