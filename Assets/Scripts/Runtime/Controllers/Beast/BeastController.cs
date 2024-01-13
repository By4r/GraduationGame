using System;
using UnityEngine;
using UnityEngine.AI;

namespace Runtime.Controllers.Beast

{
    public class BeastController : MonoBehaviour
    {
        public NavMeshAgent beast;
        public Transform player;

        private void Update()
        {
            beast.SetDestination(player.position);
            
        }
    }
}