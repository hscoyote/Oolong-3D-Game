using UnityEngine;

public class SpriteDirectonalController : MonoBehaviour
{    
    [SerializeField] Transform mainTransform;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Range(0f, 180f)][SerializeField] float backAngle = 65f;
    [Range(0f, 180f)][SerializeField] float sideAngle = 155f;
    //[SerializeField] float speedThreshold = 0.1f;
    void LateUpdate()
    {
        


        Vector3 camForwardVector = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
        float signedAngle = Vector3.SignedAngle(mainTransform.forward, camForwardVector, Vector3.up);

        Vector2 animationDirection = new Vector2(0f, -1f);

        float angle = Mathf.Abs(signedAngle);





        if (angle < backAngle)
        {
            animationDirection = new Vector2(0f, -1f);
            // spriteRenderer.flipX = false;
        }
        else if (angle < sideAngle)
        {
            animationDirection = new Vector2(1f, 0f);
            
            if (signedAngle < 0)
            {
                spriteRenderer.flipX = true;
            } else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animationDirection = new Vector2(0f, 1f);
            // spriteRenderer.flipX = signedAngle < 0f;
        }

        animator.SetFloat("MoveX", animationDirection.x);
        animator.SetFloat("MoveY", animationDirection.y);
    }
}
