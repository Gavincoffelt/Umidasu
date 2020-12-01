using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgent : MonoBehaviour
{
    private enum thingsToDo // all the categories the ai can do
    {
        home,
        sleep,
        shop,
        explore
    };

    private class allLocations // has all the locations stored
    {
        public Transform aiHouse;
        public Transform shops;
    }

    private GameObject allGoals;
    private allLocations aiLocations;
    private thingsToDo whatsCharacterDoing; // currently what the ai is doing
    private NavMeshAgent playerAgent;
    private Transform goal; // current destination for character
    private bool needTask = true; // does the character need a new task to do?

    public string home;

    void Start()
    {
        allGoals = GameObject.FindGameObjectWithTag("AllGoals");

        aiLocations.aiHouse = allGoals.transform.Find(this.name + " house");

        aiLocations.shops = allGoals.transform.Find("shops");

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
            // if night time to go to sleep
            // if not night can go out shoping or do chores at home
            // maybe go for an explore

            switch (whatsCharacterDoing)
            {
                case thingsToDo.home:
                    // if not home
                    // go home
                    // if home
                    // find something to do in the home
                    break;

                case thingsToDo.sleep:
                    // if not home
                    // go home
                    // if home
                    // sleep
                    break;

                case thingsToDo.shop:
                    // choose a shop to go to
                    // if not at the shop
                    // go to the shop
                    // if at the shop
                    // look around
                    break;

                case thingsToDo.explore:
                    // go on an adventure
                    break;

                default:
                    break;
            }



            playerAgent.destination = goal.position;
            needTask = false;
        }
        else if(new Vector3(this.transform.position.x, 0, this.transform.position.z) == new Vector3(playerAgent.destination.x, 0, playerAgent.destination.z)) // if character has reached its destination
        {
            needTask = true;
        }
    }
}