using LiteNetLib;
using UnityEngine;

namespace zettlers
{
    public class Networking : MonoBehaviour
    {

        public static readonly DeliveryMethod PacketDeliveryMethod = DeliveryMethod.ReliableUnordered;
        public static bool IsServer;
        public bool Server;

        private void Start() 
        {
            IsServer = Server;
            Debug.Log("Is server " + IsServer);
        }
    }
}
