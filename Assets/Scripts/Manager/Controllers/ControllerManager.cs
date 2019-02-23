using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Controllers
{
    public class ControllerManager : MonoBehaviour
    {
        public static ControllerManager Controller { get; set; }

        public ControllerManager()
        {
            if (Controller == null)
                Controller = this;
            else
                Destroy(this);
        }

        private void Update()
        {
            
        }
    }
}
