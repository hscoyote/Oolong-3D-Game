using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{
    // Start is called before the first frame update
    public void ReactToHit()
    {
        WanderingAI behaviour = GetComponent<WanderingAI>();
        if (behaviour != null)
        {
            behaviour.SetAlive(false);
        }
        StartCoroutine(Die());
        
    }

     //Death Animation, as a coroutine
    public IEnumerator Die()
    {
        //Have the target fall over to the side
        transform.Rotate(-75,0,0);

        //then wait
        yield return new WaitForSeconds(1.5f);
        //scream.Play();

        //then despawn by destroying itself
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
