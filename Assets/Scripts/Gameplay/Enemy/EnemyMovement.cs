using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float viewDistance;
    [SerializeField] float attackDistance;
    
    [SerializeField] float attackRange;
    [SerializeField] float attackHeight;

    GameObject player;
    float distanceToPlayer => Mathf.Abs(transform.position.x - player.transform.position.x);
    Vector2 _velocity;

    private void Start()
    {
        _velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
            //Hunt();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //CalmDown();
            player = null;
        }
    }

    IEnumerator Hunting ()
    {
        //Vector2.SmoothDamp(transform.position, new Vector2(player.position))
        yield return null;
    }

    IEnumerator Fighting ()
    {
        yield return null;
    }
}
