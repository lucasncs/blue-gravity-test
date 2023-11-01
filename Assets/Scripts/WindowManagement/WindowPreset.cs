using UnityEngine;

namespace WindowManagement
{
    [CreateAssetMenu(menuName = "Window Management/Window Preset", fileName = "WindowPreset")]
    public class WindowPreset : ScriptableObject
    {
        [SerializeField] private WindowType _type;
        [SerializeField] private AWindowController _window;

        public AWindowController Window => _window;
        public WindowType Type => _type;
    }
}