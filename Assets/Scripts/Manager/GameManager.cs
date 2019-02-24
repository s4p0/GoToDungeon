using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            Messages = new MessageCollection();
        }
        
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


}
