using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Material carMaterial;
    public Color carColor;

    public bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        carColor = carMaterial.GetColor("_Color");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CarStarted()
    {
        canMove = true;
    }

    public void CarOff()
    {
        canMove = false;
    }

    public void MoveCar(float speed)
    {
        if (canMove)
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void TurnCar(float degree)
    {
        if (canMove)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.x = rot.z = 0;
            rot.y = degree;
            transform.rotation = Quaternion.Euler(rot);
        }
    }

    public void ChangeCarColor(float value)
    {
        carColor.b = value;
        // carMaterial.SetColor("_Color", carColor);
        carMaterial.color = carColor;
    }

    public void JumpCar()
    {
        GetComponent<Rigidbody>().AddForce(transform.up * 3, ForceMode.Impulse);
    }
}
