using UnityEngine;

public partial class Unit : MonoBehaviour
{
    protected NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent
    {
        get
        {
            if (_navAgent == null)
            {
                _navAgent = this.GetComponent<NavMeshAgent>();
                if (_navAgent == null)
                    _navAgent = this.gameObject.AddComponent<NavMeshAgent>();
            }
            return _navAgent;
        }
    }
    public bool isNaving = false; // 是否寻路中

    public void moveTo(Vector3 destination)
    {
        NavAgent.SetDestination(destination);
    }

    public void faceToTarget(Vector3 targetPos)
    {
        m_cacheTransform.forward = targetPos - m_cacheTransform.localPosition;
    }

    public void updateMove()
    { 
        
    }
}

