using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private GameObject hitboxHolder;
    private Transform[] hitboxes;
    private List<Vector3> hitboxStandartPositions;
    private List<Vector3> hitboxFlippedPositions;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
        hitboxes = hitboxHolder.GetComponentsInChildren<Transform>();
        
        hitboxStandartPositions = new List<Vector3>();
        hitboxFlippedPositions = new List<Vector3>();
    
        foreach (Transform transform in hitboxes)
        {
            hitboxStandartPositions.Add(transform.localPosition);
            hitboxFlippedPositions.Add(new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z));
        }
    }

    void LateUpdate()
    {
        if (myRigidbody.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
            for (int i = 0; i< hitboxes.Length; i++)
            {
                hitboxes[i].localPosition = hitboxStandartPositions[i];
            }
        }

        if (myRigidbody.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
            for (int i = 0; i < hitboxes.Length; i++)
            {
                hitboxes[i].localPosition = hitboxFlippedPositions[i];
            }
        }

    }
}
