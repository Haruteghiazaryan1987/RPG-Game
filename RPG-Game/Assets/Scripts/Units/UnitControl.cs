using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class UnitControl : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Vector3 target;
        private bool haveАTarget;
        private NavMeshObstacle obstacle;

        void Awake(){
            agent = GetComponent<NavMeshAgent>();
            obstacle = GetComponent<NavMeshObstacle>();
        }

        public void GoToPositions(Vector3 pos){
            agent.SetDestination(pos);
        }

        public void GoToPositionIfInits(Vector3 pos){
            agent.SetDestination(pos);
        }

        public IEnumerator EnableUnitMovementCoroutine(){
            if (obstacle.enabled){
                obstacle.enabled = false;
            }

            for (int i = 0; i < 2; i++){
                yield return new WaitForEndOfFrame();
            }

            if (!agent.enabled){
                agent.enabled = true;
            }
            Debug.Log("mtav");

            haveАTarget = true;
        }
    }
}