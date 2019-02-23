using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Weapons;

namespace Assets.Scripts.Events
{
    public class WeaponPickedUpEvent : IGlobalEvent
    {
        public Weapon Weapon { get; internal set; }
        public Character Character { get; internal set; }
        public Collider2D Collision { get; internal set; }
        public PickUpWeapon PickUpWeapon { get; internal set; }
    }
}
