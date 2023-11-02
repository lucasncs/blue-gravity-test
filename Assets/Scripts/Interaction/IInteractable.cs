using UnityEngine;

namespace Interaction
{
    public interface IInteractable
    {
        void Interact(IInteractor interactor);
    }

    public interface IInteractor
    {
        GameObject GetGameObject();
    }
}