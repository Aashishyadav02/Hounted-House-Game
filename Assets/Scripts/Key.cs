using System;
using UnityEngine;

namespace StealthGame
{
    public class Key : MonoBehaviour
    {
        public string KeyName = "RedKey";
        
        private void OnTriggerEnter(Collider other)
        {
            
            PlayerMovements player = other.gameObject.GetComponent<PlayerMovements>();
            Debug.Log("Player touched the key");
            //this wasn't a player
            if (player == null)
                return;
        
            player.AddKey(KeyName);
            Destroy(gameObject);
        }
    }
}