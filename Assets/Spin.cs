using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.forward, this.rotSpeed * Time.deltaTime);
    }
}
