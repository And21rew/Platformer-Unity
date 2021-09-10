using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera1 : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform queen;
    public bool check = false;
    readonly float speed = 8f;

    void Start()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void Update()
    {
        if (check)
        {
            Vector3 position = queen.position;
            position.z = transform.position.z;
            //transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime);
            transform.position = position;
        }
        else 
        {
            Vector3 position = target.position;
            position.z = transform.position.z;
            transform.position = Vector3.Lerp(transform.position, position, speed * Time.deltaTime); 
        }
    }
}