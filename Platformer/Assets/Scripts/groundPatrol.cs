using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPatrol : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private bool moveLeft = true;
    [SerializeField] private Transform groundDetect;

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);
        if (!groundInfo.collider)
        {
            if (moveLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveLeft = true;
            }
        }
    }
}
