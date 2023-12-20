using UnityEngine;

public class BuoyantScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;

    public float airDrag = 0f;
    public float airAngularDrag = 0.05f;

    public float buoyancyForce = 10f;

    private Rigidbody rb;

    private bool hasTouchedWater;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float diffy = transform.position.y;
        bool isUnderwater = diffy < 0;

        if(isUnderwater)
        {
            hasTouchedWater = true;
        }

        if(!hasTouchedWater)
        {
            return;
        }

        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * buoyancyForce * -diffy;
            rb.AddForce(vector, ForceMode.Acceleration);
        }

        rb.drag = isUnderwater ? underwaterDrag : airDrag;
        rb.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}