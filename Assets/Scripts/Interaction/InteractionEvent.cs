using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class InteractionEvent : MonoBehaviour, IInteractable
    {
        public UnityEvent OnInteraction;

        public void Interact()
        {
            OnInteraction.Invoke();
        }
    }
}