using System;
using Broadcaster;
using UnityEngine;

namespace WindowManagement
{
    public class WindowManagementBroadcaster : ABroadcaster<WindowManagementBroadcaster, IWindowManagementMessage>
    {
    }

    public interface IWindowManagementMessageListener<T> : IMessageListener<T> where T : IWindowManagementMessage
    {
    }

    public interface IWindowManagementMessage : IBroadcasterMessage
    {
    }

    public struct ShowWindowMessage : IWindowManagementMessage
    {
        public readonly WindowType WindowType;
        public readonly Action OnCloseAction;
        public readonly IWindowIntent Intent;
        public readonly Transform ParentTransform;

        public ShowWindowMessage(
            WindowType windowType,
            Action onCloseAction = null,
            IWindowIntent intent = null,
            Transform parentTransform = null)
        {
            WindowType = windowType;
            OnCloseAction = onCloseAction;
            Intent = intent;
            ParentTransform = parentTransform;
        }
    }

    public struct CloseWindowMessage : IWindowManagementMessage
    {
    }
}