using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterEvent : MonoBehaviour
{

    public string _tag = "Player";

    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tag))
            OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tag))
            OnExit.Invoke();
    }

}
