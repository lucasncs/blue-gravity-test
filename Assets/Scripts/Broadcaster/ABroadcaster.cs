using System;
using System.Collections.Generic;
using UnityEngine;

namespace Broadcaster
{
    public abstract class ABroadcaster<TClass, TMessage>
        where TClass : class, new()
        where TMessage : IBroadcasterMessage
    {
        private static TClass _instance;
        public static TClass Instance => _instance ??= new TClass();

        private readonly HashSet<object> _listeners = new();

        public void Subscribe<T>(IMessageListener<T> listener) where T : TMessage
        {
            _listeners.Add(listener);
        }

        public void Unsubscribe<T>(IMessageListener<T> listener) where T : TMessage
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }

        public void Broadcast<T>(T message) where T : TMessage
        {
            foreach (object subscriber in _listeners)
            {
                try
                {
                    if (subscriber is IMessageListener<T> subs)
                    {
                        subs.OnMessageReceived(message);
                    }
                }
                catch (InvalidCastException invalidCastException)
                {
                    Debug.LogException(invalidCastException);
                }
                catch (Exception e)
                {
                    Debug.LogError(
                        $"[{typeof(T).Name}] Error: {e.Message} \n StackTrace: {e.StackTrace}\n InnerException: {e.InnerException}");
                }
            }
        }
    }

    public interface IMessageListener<T>
    {
        void OnMessageReceived(T message);
    }

    public interface IBroadcasterMessage
    {
    }
}