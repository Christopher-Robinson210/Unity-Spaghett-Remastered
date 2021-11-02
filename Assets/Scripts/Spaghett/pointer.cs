using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointer : MonoBehaviour
{
    public GameObject spaghett;
    public float magnitude;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        magnitude = (transform.position - spaghett.transform.position).magnitude;
        print($"Distance: {(transform.position - spaghett.transform.position).magnitude}");
    }
}
