using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Weapons
{
    [System.Serializable]
    public class WeaponMovementController
    {
        public WeaponMovementEnum movement;
        public AnimatorOverrideController controller;
    }
}
