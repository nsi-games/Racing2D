using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Racing2D
{
    public class Car : MonoBehaviour
    {
        public float acceleration;
        public float steering;
        public Rigidbody2D rigid;
        
        void FixedUpdate()
        {
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");

            float acc = inputV * acceleration;
            rigid.AddForce(transform.up * acc);

            float direction = Vector2.Dot(rigid.velocity, rigid.GetRelativeVector(Vector2.up));
            if (direction >= 0.0f)
            {
                rigid.rotation -= inputH * steering * (rigid.velocity.magnitude / 5.0f);
                //rb.AddTorque((h * steering) * (rb.velocity.magnitude / 10.0f));
            }
            else
            {
                rigid.rotation += inputH * steering * (rigid.velocity.magnitude / 5.0f);
                //rb.AddTorque((-h * steering) * (rb.velocity.magnitude / 10.0f));
            }

            Vector2 forward = new Vector2(0.0f, 0.5f);
            float steeringRightAngle;
            if (rigid.angularVelocity > 0)
            {
                steeringRightAngle = -90;
            }
            else
            {
                steeringRightAngle = 90;
            }

            Vector2 rightAngleFromForward = Quaternion.AngleAxis(steeringRightAngle, Vector3.forward) * forward;
            Debug.DrawLine(rigid.position, rigid.GetRelativePoint(rightAngleFromForward), Color.green);
            float driftForce = Vector2.Dot(rigid.velocity, rigid.GetRelativeVector(rightAngleFromForward.normalized));
            Vector2 relativeForce = (rightAngleFromForward.normalized * -1.0f) * (driftForce * 10.0f);
            Debug.DrawLine(rigid.position, rigid.GetRelativePoint(relativeForce), Color.red);
            rigid.AddForce(rigid.GetRelativeVector(relativeForce));
        }
    }
}