using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FollowAI : MonoBehaviour
{
    NavMeshAgent _agent;
    Transform _player;
    ActiveRagdollBone _ragdoll;

    Coroutine _deathCorotuine;

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

    public void Kill(float seconds = 5)
    {
        if (_deathCorotuine != null)
        {
            StopCoroutine(_deathCorotuine);
            _deathCorotuine = null;
        }

        _deathCorotuine = StartCoroutine(KillAnim(seconds));
    }

    IEnumerator KillAnim(float seconds)
    {
        _ragdoll.enabled = false;
        _agent.isStopped = true;
        yield return new WaitForSeconds(seconds);
        _ragdoll.enabled = true;
        _agent.isStopped = false;
    }
}
