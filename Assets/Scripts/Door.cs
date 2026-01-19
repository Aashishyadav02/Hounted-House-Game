using System;
using UnityEngine;

namespace StealthGame
{
    public class Door : MonoBehaviour
    {
        public string KeyName = "RedKey";
    
        private void OnCollisionEnter(Collision other)
        {
            PlayerMovements player = other.gameObject.GetComponent<PlayerMovements>();

            if (player == null)
                return;

            if (player.OwnKey(KeyName))
            {
                Destroy(gameObject);
            }
        }
    }
}