using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmeraScript : MonoBehaviour
{
    public GameObject dino;
    Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - dino.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = offset + dino.transform.position;
    }
}