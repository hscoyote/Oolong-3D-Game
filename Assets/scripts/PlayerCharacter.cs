using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
     private int health;
     private RayShooter RayShooter;
     private RelativeMovement relativeMovement;


    // Start is called before the first frame update
    void Start()
    {
        health = 2;

        //reference to the Relative Movement component
        relativeMovement = GetComponent<RelativeMovement>();

        //reference to rayshooter component
        RayShooter = GetComponentInChildren<RayShooter>();


        
    }

     public void Hurt(int damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, 2);
        Debug.Log($"Health:  {health}");

        //
        if (health == 0)
        {
            relativeMovement.SetMovement(false);
            RayShooter.SetShooting(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
