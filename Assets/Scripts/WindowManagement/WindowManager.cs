using System;
using UnityEngine;

namespace WindowManagement
{
    public class WindowManager : MonoBehaviour,
        IWindowManagementMessageListener<ShowWindowMessage>,
        IWindowManagementMessageListener<CloseWindowMessage>
    {
        public static WindowManager Instance { get; private set; }

        [SerializeField] private Camera _uiCamera;
        [SerializeField] private WindowCollection _windowCollection;

        private WindowStruct _currentOpenedWindow;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            WindowManagementBroadcaster.Instance.Subscribe<ShowWindowMessage>(this);
            WindowManagementBroadcaster.Instance.Subscribe<CloseWindowMessage>(this);
        }

        private void OnDestroy()
        {
            WindowManagementBroadcaster.Instance.Unsubscribe<ShowWindowMessage>(this);
            WindowManagementBroadcaster.Instance.Unsubscribe<CloseWindowMessage>(this);
        }

        public AWindowController ShowWindow(WindowType windowType,
            Action onCloseAction = null,
            IWindowIntent intent = null,
            Transform parentTransform = null)
        {
            if (windowType == WindowType.None || _windowCollection == null) return null;

            WindowPreset preset = GetWindowPreset(windowType);
            if (preset == null) return null;

            CloseWindow(_currentOpenedWindow);

            AWindowController newWindow = InstantiateWindow(preset, parentTransform);
            _currentOpenedWindow = new WindowStruct(preset, newWindow, onCloseAction);

            newWindow.Show(intent);

            return newWindow;
        }

        private WindowPreset GetWindowPreset(WindowType windowType)
        {
            return _windowCollection != null ? _windowCollection.Find(window => window.Type == windowType) : null;
        }

        private void CloseWindow(WindowStruct windowStruct)
        {
            if (windowStruct.Reference == null) return;

            windowStruct.OnCloseCallback?.Invoke();
            windowStruct.Reference.OnCloseWindow();
            Destroy(windowStruct.Reference.gameObject);
        }

        private AWindowController InstantiateWindow(WindowPreset preset, Transform parentTransform)
        {
            AWindowController window = Instantiate(preset.Window, parentTransform);
            var canvas = window.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = _uiCamera;

            return window;
        }

        public AWindowController GetCurrentWindow()
        {
            return _currentOpenedWindow.Reference;
        }

        public void CloseCurrentWindow()
        {
            CloseWindow(_currentOpenedWindow);
        }

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Escape)) return;
            CloseCurrentWindow();
        }

        private struct WindowStruct
        {
            public readonly WindowPreset Preset;
            public readonly AWindowController Reference;
            public readonly Action OnCloseCallback;

            public WindowStruct(WindowPreset preset, AWindowController reference, Action onCloseCallback)
            {
                Preset = preset;
                Reference = reference;
                OnCloseCallback = onCloseCallback;
            }
        }

        public void OnMessageReceived(ShowWindowMessage message)
        {
            ShowWindow(message.WindowType,
                message.OnCloseAction,
                message.Intent,
                message.ParentTransform);
        }

        public void OnMessageReceived(CloseWindowMessage message)
        {
            CloseCurrentWindow();
        }
    }
}