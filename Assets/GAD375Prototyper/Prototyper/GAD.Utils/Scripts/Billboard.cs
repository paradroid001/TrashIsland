using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GAD.Utils
{

    public class Billboard : MonoBehaviour
    {
        public bool invert = true;
        public bool lockX = true;
        public bool lockZ = true;

        // Update is called once per frame
        void LateUpdate()
        {
            //transform.LookAt(Camera.main.transform, Vector3.up);
            transform.LookAt(Camera.main.transform,/*transform.position + (Camera.main.transform.rotation * (Vector3.forward * facing)),*/
                Camera.main.transform.rotation * Vector3.up);
            Vector3 eulerAngles = transform.eulerAngles;
            if (lockZ)
                eulerAngles.z = 0;
            if (lockX)
                eulerAngles.x = 0;
            if (invert)
                eulerAngles.y += 180.0f;
            transform.eulerAngles = eulerAngles; 
        }
    }
}