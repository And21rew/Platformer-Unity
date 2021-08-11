using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlant : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float TimeToDisable = 4f;

    void Start()
    {
        StartCoroutine(SetDisable());
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
    }

    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(TimeToDisable);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            StopCoroutine(SetDisable());
            gameObject.SetActive(false);
    }
}
