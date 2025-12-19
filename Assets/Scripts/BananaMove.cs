using UnityEngine;

public class BananaMove : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 30, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(transform.forward * 10f * Time.deltaTime, ForceMode.Force);
        rb.AddForce(transform.forward * 130, ForceMode.Force);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(""))
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "")
        {

        }
    }


}
