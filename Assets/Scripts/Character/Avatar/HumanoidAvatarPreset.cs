using Items.Avatar;
using UnityEngine;

namespace Character.Avatar
{
    [CreateAssetMenu(menuName = "Humanoid Avatar Preset", fileName = nameof(HumanoidAvatarPreset))]
    public class HumanoidAvatarPreset : ScriptableObject
    {
        [SerializeField] private HatItem _hatItem;
        [SerializeField] private HeadFaceItem _faceItem;
        [SerializeField] private UpperBodyItem _upperBodyItem;
        [SerializeField] private HandsItem _handsItem;
        [SerializeField] private LowerBodyItem _lowerBodyItem;
        [SerializeField] private FeetItem _feetItem;

        public HatItem HatItem => _hatItem;
        public HeadFaceItem HeadFaceItem => _faceItem;
        public UpperBodyItem UpperBodyItem => _upperBodyItem;
        public HandsItem HandsItem => _handsItem;
        public LowerBodyItem LowerBodyItem => _lowerBodyItem;
        public FeetItem FeetItem => _feetItem;
    }
}