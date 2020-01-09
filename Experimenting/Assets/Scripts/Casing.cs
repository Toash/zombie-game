using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.right * 8, ForceMode.Impulse);
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
