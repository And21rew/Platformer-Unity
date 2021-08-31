using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shoot;
    [SerializeField] private float timeShoot = 1f;
    //private Animator AnimationPlant;

    void Start()
    {
        //AnimationPlant = GetComponent<Animator>();
        StartCoroutine(Shooting());
    }

    IEnumerator Shooting()
    {
        //AnimationPlant.SetBool("Shoot", false);
        yield return new WaitForSeconds(timeShoot);
        //AnimationPlant.SetBool("Shoot", true);
        Instantiate(bullet, shoot.transform.position, transform.rotation);
        StartCoroutine(Shooting());
    }
}
