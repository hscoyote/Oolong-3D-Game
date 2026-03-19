using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    
    //fields for the fireball prefab and current instance 
    [SerializeField] GameObject fireballPrefab;
    
    private GameObject fireball;

    //speed of the wandering game object
    //and how far to look for obstacles
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;


    //state of the game object
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        //set the game object to alive 
        isAlive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        //move forward only if alive
        if (isAlive)
        {
            transform.Translate(0,0, speed * Time.deltaTime);
        }  


        //move forward
        transform.Translate(0,0, speed * Time.deltaTime);

        //create a ray from the wandering game object, pointed in
        //the same direction as the game object
        Ray ray = new Ray(transform.position, transform.forward);

        //data container containing hit information 
        RaycastHit hit;

        //perform a raycast with a circular volume around the ray
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            //get a reference to the game object that was hit 
            //by the sphere cast
            GameObject hitObject = hit.transform.gameObject;

            //If the object hit was the player, shoot a fireball
            ///otherwise, if it wasn't the player and its within the 
            /// obstacle eane, turn around a random angle
            if (hitObject.GetComponent<PlayerCharacter>())
            {
                if (fireball == null)
                {
                    fireball = Instantiate(fireballPrefab) as GameObject;
                    fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                    fireball.transform.rotation = transform.rotation;
                }

            } else if(hit.distance < obstacleRange) {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                    
                }

        }


        
    }

     public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}
