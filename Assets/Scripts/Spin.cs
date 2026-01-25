using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotSpeed;
    public bool spinAroundSomething;
    public Transform spinAroundPos;

    void Update()
    {

        
        if (spinAroundSomething)
        {
            transform.RotateAround(spinAroundPos.position, Vector3.forward, this.rotSpeed * Time.deltaTime);
        }
        else
        {
            transform.RotateAround(transform.position, Vector3.forward, this.rotSpeed * Time.deltaTime);
        }
    }
}
