using System;
using Items;
using Items.Avatar;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Character.Avatar
{
    public class HumanoidCharacterAvatarController : ACharacterAvatarController
    {
        [Header("Avatar Renderers")]

        [SerializeField] private SpriteRenderer _rendHead;
        [SerializeField] private SpriteRenderer _rendFace;
        [SerializeField] private SpriteRenderer _rendHat;
        [SerializeField] private SpriteRenderer _rendWristLeft;
        [SerializeField] private SpriteRenderer _rendWeaponLeft;
        [SerializeField] private SpriteRenderer _rendElbowLeft;
        [SerializeField] private SpriteRenderer _rendShoulderLeft;
        [SerializeField] private SpriteRenderer _rendWristRight;
        [SerializeField] private SpriteRenderer _rendWeaponRight;
        [SerializeField] private SpriteRenderer _rendElbowRight;
        [SerializeField] private SpriteRenderer _rendShoulderRight;
        [SerializeField] private SpriteRenderer _rendTorso;
        [SerializeField] private SpriteRenderer _rendFootLeft;
        [SerializeField] private SpriteRenderer _rendLegLeft;
        [SerializeField] private SpriteRenderer _rendFootRight;
        [SerializeField] private SpriteRenderer _rendLegRight;
        [SerializeField] private SpriteRenderer _rendPelvis;

        [Header("Default Avatar (Optional)")]

        [SerializeField] private HumanoidAvatarPreset _defaultAvatar;
        [SerializeField] private bool _loadPresetOnAwake = true;

        private AsyncOperationHandle<Sprite> _handleHead;
        private AsyncOperationHandle<Sprite> _handleFace;
        private AsyncOperationHandle<Sprite> _handleHat;
        private AsyncOperationHandle<Sprite> _handleWristLeft;
        private AsyncOperationHandle<Sprite> _handleWeaponLeft;
        private AsyncOperationHandle<Sprite> _handleElbowLeft;
        private AsyncOperationHandle<Sprite> _handleShoulderLeft;
        private AsyncOperationHandle<Sprite> _handleWristRight;
        private AsyncOperationHandle<Sprite> _handleWeaponRight;
        private AsyncOperationHandle<Sprite> _handleElbowRight;
        private AsyncOperationHandle<Sprite> _handleShoulderRight;
        private AsyncOperationHandle<Sprite> _handleTorso;
        private AsyncOperationHandle<Sprite> _handleFootLeft;
        private AsyncOperationHandle<Sprite> _handleLegLeft;
        private AsyncOperationHandle<Sprite> _handleFootRight;
        private AsyncOperationHandle<Sprite> _handleLegRight;
        private AsyncOperationHandle<Sprite> _handlePelvis;

        private void Awake()
        {
            if (!_loadPresetOnAwake || _defaultAvatar == null) return;

            ApplyAvatarItem(_defaultAvatar.HatItem);
            ApplyAvatarItem(_defaultAvatar.HeadFaceItem);
            ApplyAvatarItem(_defaultAvatar.UpperBodyItem);
            ApplyAvatarItem(_defaultAvatar.HandsItem);
            ApplyAvatarItem(_defaultAvatar.LowerBodyItem);
            ApplyAvatarItem(_defaultAvatar.FeetItem);
        }

        public override void ApplyAvatarItem(AAvatarItem avatarItem)
        {
            if (avatarItem == null) return;

            switch (avatarItem.SlotType)
            {
                case ItemSlotType.Hat:
                    ApplyHat(avatarItem as HatItem);
                    break;
                case ItemSlotType.HeadFace:
                    ApplyHeadFace(avatarItem as HeadFaceItem);
                    break;
                case ItemSlotType.UpperBody:
                    ApplyUpperBody(avatarItem as UpperBodyItem);
                    break;
                case ItemSlotType.Hands:
                    ApplyHands(avatarItem as HandsItem);
                    break;
                case ItemSlotType.LowerBody:
                    ApplyLowerBody(avatarItem as LowerBodyItem);
                    break;
                case ItemSlotType.Feet:
                    ApplyFeet(avatarItem as FeetItem);
                    break;
                case ItemSlotType.Holdable:
                    ApplyWeapon(avatarItem as WeaponItem);
                    break;
                case ItemSlotType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void RemoveAvatarItem(ItemSlotType slotType)
        {
            if (_defaultAvatar == null) return;

            switch (slotType)
            {
                case ItemSlotType.Hat:
                    ApplyHat(_defaultAvatar.HatItem);
                    break;
                case ItemSlotType.HeadFace:
                    ApplyHeadFace(_defaultAvatar.HeadFaceItem);
                    break;
                case ItemSlotType.UpperBody:
                    ApplyUpperBody(_defaultAvatar.UpperBodyItem);
                    break;
                case ItemSlotType.Hands:
                    ApplyHands(_defaultAvatar.HandsItem);
                    break;
                case ItemSlotType.LowerBody:
                    ApplyLowerBody(_defaultAvatar.LowerBodyItem);
                    break;
                case ItemSlotType.Feet:
                    ApplyFeet(_defaultAvatar.FeetItem);
                    break;
                case ItemSlotType.Holdable:
                    ApplyWeapon(_defaultAvatar.WeaponItem);
                    break;
                case ItemSlotType.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ApplyHat(HatItem item)
        {
            LoadAssetToRenderer(_rendHat, item.SpriteAsset, ref _handleHat);
        }

        private void ApplyHeadFace(HeadFaceItem item)
        {
            LoadAssetToRenderer(_rendFace, item.SpriteAsset, ref _handleFace);
        }

        private void ApplyUpperBody(UpperBodyItem item)
        {
            LoadAssetToRenderer(_rendTorso, item.TorsoSpriteAsset, ref _handleTorso);
            LoadAssetToRenderer(_rendShoulderLeft, item.ShoulderLeftSpriteAsset, ref _handleShoulderLeft);
            LoadAssetToRenderer(_rendShoulderRight, item.ShoulderRightSpriteAsset, ref _handleShoulderRight);
        }

        private void ApplyHands(HandsItem item)
        {
            LoadAssetToRenderer(_rendWristLeft, item.WristLeftSpriteAsset, ref _handleWristLeft);
            LoadAssetToRenderer(_rendElbowLeft, item.ElbowLeftSpriteAsset, ref _handleElbowLeft);
            LoadAssetToRenderer(_rendWristRight, item.WristRightSpriteAsset, ref _handleWristRight);
            LoadAssetToRenderer(_rendElbowRight, item.ElbowRightSpriteAsset, ref _handleElbowRight);
        }

        private void ApplyLowerBody(LowerBodyItem item)
        {
            LoadAssetToRenderer(_rendPelvis, item.PelvisSpriteAsset, ref _handlePelvis);
            LoadAssetToRenderer(_rendLegLeft, item.LegLeftSpriteAsset, ref _handleLegLeft);
            LoadAssetToRenderer(_rendLegRight, item.LegRightSpriteAsset, ref _handleLegRight);
        }

        private void ApplyFeet(FeetItem item)
        {
            LoadAssetToRenderer(_rendFootLeft, item.FootLeftSpriteAsset, ref _handleFootLeft);
            LoadAssetToRenderer(_rendFootRight, item.FootRightSpriteAsset, ref _handleFootRight);
        }

        private void ApplyWeapon(WeaponItem weaponItem)
        {
            LoadAssetToRenderer(_rendWeaponLeft, weaponItem.SpriteAsset, ref _handleWeaponLeft);
            LoadAssetToRenderer(_rendWeaponRight, weaponItem.SpriteAsset, ref _handleWeaponRight);
        }

        private void LoadAssetToRenderer(SpriteRenderer rend, AssetReferenceSprite asset,
            ref AsyncOperationHandle<Sprite> handle)
        {
            DisposeHandle(handle);
            handle = asset.LoadAssetAsync();
            rend.sprite = handle.WaitForCompletion();
        }

        private void DisposeHandle(AsyncOperationHandle<Sprite> handle)
        {
            if (!handle.IsValid()) return;
            Addressables.Release(handle);
        }

        private void OnDestroy()
        {
            DisposeAllHandles();
        }

        private void DisposeAllHandles()
        {
            DisposeHandle(_handleHead);
            DisposeHandle(_handleFace);
            DisposeHandle(_handleHat);
            DisposeHandle(_handleWristLeft);
            DisposeHandle(_handleWeaponLeft);
            DisposeHandle(_handleElbowLeft);
            DisposeHandle(_handleShoulderLeft);
            DisposeHandle(_handleWristRight);
            DisposeHandle(_handleWeaponRight);
            DisposeHandle(_handleElbowRight);
            DisposeHandle(_handleShoulderRight);
            DisposeHandle(_handleTorso);
            DisposeHandle(_handleFootLeft);
            DisposeHandle(_handleLegLeft);
            DisposeHandle(_handleFootRight);
            DisposeHandle(_handleLegRight);
            DisposeHandle(_handlePelvis);
        }
    }
}