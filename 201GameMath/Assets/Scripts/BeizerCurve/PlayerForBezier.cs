using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerForBezier : MonoBehaviour
{
    public GameObject nyaNyaNya;
    public Transform target;
    void Update()
    {
        
    }
    public void OnAttack(InputValue input)
    {
        Shooting();
    }
    
    public void Shooting()
    {
        for (int i = 0; i < 10; i++)
        {
            Bezier bezier = Instantiate(nyaNyaNya, transform.position, Quaternion.identity).GetComponent<Bezier>();
            bezier.p0 = this.transform;
            bezier.p3 = target;
            bezier.StartShooting();
        }
    }
    

}
