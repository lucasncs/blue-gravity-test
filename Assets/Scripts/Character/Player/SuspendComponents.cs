using UnityEngine;
using UnityEngine.Events;
using WindowManagement;

namespace Character.Player
{
    public class SuspendComponents : MonoBehaviour,
        IWindowManagementMessageListener<ShowWindowMessage>,
        IWindowManagementMessageListener<CloseWindowMessage>
    {
        [SerializeField] private UnityEvent<bool> _setComponentsActive;

        private void Awake()
        {
            WindowManagementBroadcaster.Instance.Subscribe<ShowWindowMessage>(this);
            WindowManagementBroadcaster.Instance.Subscribe<CloseWindowMessage>(this);
        }

        private void OnDestroy()
        {
            WindowManagementBroadcaster.Instance.Unsubscribe<ShowWindowMessage>(this);
            WindowManagementBroadcaster.Instance.Unsubscribe<CloseWindowMessage>(this);
        }

        public void OnMessageReceived(ShowWindowMessage message)
        {
            _setComponentsActive.Invoke(false);
        }

        public void OnMessageReceived(CloseWindowMessage message)
        {
            _setComponentsActive.Invoke(true);
        }
    }
}