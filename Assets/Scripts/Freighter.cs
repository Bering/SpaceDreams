using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freighter : MonoBehaviour
{
    [SerializeField] float speed = 1;

    Vector3 movement;

    // Update is called once per frame
    void Update()
    {
        movement = transform.forward * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
