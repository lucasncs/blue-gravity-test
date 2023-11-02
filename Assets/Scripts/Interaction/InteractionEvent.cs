using UnityEngine;
using UnityEngine.Events;

namespace Interaction
{
    public class InteractionEvent : MonoBehaviour, IInteractable
    {
        public UnityEvent<IInteractor> OnInteraction;

        public void Interact(IInteractor interactor)
        {
            OnInteraction.Invoke(interactor);
        }
    }
}