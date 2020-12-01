using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private List<Transform> goal = new List<Transform>(); // list of all possible goals to pick from
    private bool needTask = true; // does the character need a new task to do?

    void Start()
    {
        var tempGameObject = GameObject.FindGameObjectsWithTag("goals");

        for (int i = 0; i < tempGameObject.Length; i++)
        {
            goal.Add(tempGameObject[i].transform);
        }

        if(TryGetComponent(out NavMeshAgent _mesh)) 
        {
            playerAgent = _mesh;
        }
        else 
        {
            Debug.LogError("Character " + this.name + " does not have a NavMeshAgent!"); 
        }
    }

    private void Update()
    {
        if(needTask) // if has no task yet
        {
            int i = Random.Range(0, goal.Count);

            playerAgent.destination = goal[i].position;
            needTask = false;
        }
        else if(new Vector3(this.transform.position.x, 0, this.transform.position.z) == new Vector3(playerAgent.destination.x, 0, playerAgent.destination.z)) // if character has reached its destination
        {
            needTask = true;
        }
    }
}