using UnityEngine;

namespace WindowManagement
{
    [RequireComponent(typeof(Canvas))]
    public abstract class AWindowController : MonoBehaviour
    {
        protected internal abstract void Show(IWindowIntent intent = null);
        protected internal abstract void OnCloseWindow();
    }

    public interface IWindowIntent
    {
    }
}