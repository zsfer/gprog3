using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _player;
    ActiveRagdollBone _ragdoll;

    void Start()
    {
        _agent = GetComponentInChildren<NavMeshAgent>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _ragdoll = GetComponent<ActiveRagdollBone>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_player.position);
    }

    public void Kill()
    {
        StartCoroutine(KillAnim());
    }

    IEnumerator KillAnim()
    {
        _ragdoll.enabled = false;
        _agent.isStopped = true;
        yield return new WaitForSeconds(5);
        _ragdoll.enabled = true;
        _agent.isStopped = false;
    }
}
