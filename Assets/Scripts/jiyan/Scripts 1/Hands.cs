using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Knife.RealBlood.SimpleController
{
    /// <summary>
    /// Player Hands behaviour
    /// </summary>
    public class Hands : MonoBehaviour
    {
        
        public Camera Cam;
        

        float startFov;

        void Start()
        {
            startFov = Cam.fieldOfView;
            
        }

        void Update()
        {
            

            Cam.fieldOfView = startFov;
        }
    }
}