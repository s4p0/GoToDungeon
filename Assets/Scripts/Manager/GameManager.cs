using Assets.Scripts.Pickups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Weapons;

namespace Assets.Scripts.Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Manager;

        private void Awake()
        {
            if (Manager != null)
                Destroy(this);
            else
                Manager = this;
        }

        public GameManager()
        {
            Pickups = new PickupCollection();
            Messages = new MessageCollection();
        }

        public PickupCollection Pickups { get; }
        public MessageCollection Messages { get; }

    }

    public class MessageCollection
    {
        private Dictionary<string, List<Action<object>>> list = new Dictionary<string, List<Action<object>>>();
        public void Subscribe(string trigger, Action<object> command)
        {
            if (!list.TryGetValue(trigger, out List<Action<object>> commands))
                list.Add(trigger, (commands = new List<Action<object>>()));
            commands.Add(command);                
        }

        public void Publish(string trigger, object status)
        {
            if (list.TryGetValue(trigger, out List<Action<object>> commands))
                foreach (var item in commands)
                    item.Invoke(status);
        }
    }

    public class PickupEventArgs : EventArgs
    {
        public Collider2D Collision { get; internal set; }
    }

    public class WeaponPickupEventArgs : PickupEventArgs
    {
        public Character Character;

        public PickUpWeapon WeaponPickup { get; internal set; }
        public Weapon Weapon { get; internal set; }
    }
    

    public partial class PickupCollection
    {

        private Dictionary<PickupEnum, List<EventHandler<PickupEventArgs>>> events = new Dictionary<PickupEnum, List<EventHandler<PickupEventArgs>>>();

        public void Subscribe(PickupEnum pickupEnum, EventHandler<PickupEventArgs> handler)
        {
            if (!events.TryGetValue(pickupEnum, out List<EventHandler<PickupEventArgs>> list))
            {
                list = new List<EventHandler<PickupEventArgs>>();
                events.Add(pickupEnum, list);
            }
            list.Add(handler);
        }

        public void Usubscribe(PickupEnum pickupEnum, EventHandler<PickupEventArgs> handler)
        {
            if (events.TryGetValue(pickupEnum, out List<EventHandler<PickupEventArgs>> list))
                list.Remove(handler);
        }

        public void Publish(PickupEnum pickupEnum, PickUp sender, PickupEventArgs arguments)
        {
            if (events.TryGetValue(pickupEnum, out List<EventHandler<PickupEventArgs>> list))
                foreach (var item in list)
                    item.Invoke(sender, arguments);
        }

    }
}
