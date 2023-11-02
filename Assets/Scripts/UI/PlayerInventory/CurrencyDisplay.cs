using System;
using Character.Player.InventoryManagement;
using Items;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace UI.PlayerInventory
{
    public class CurrencyDisplay : MonoBehaviour,
        IPlayerInventoryMessageListener<PlayerCurrencyUpdateMessage>
    {
        [SerializeField] private CurrencyItem _currency;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _amount;

        private AsyncOperationHandle<Sprite> _iconHandle;

        private void Awake()
        {
            if (_iconHandle.IsValid()) Addressables.Release(_iconHandle);

            _icon.enabled = false;
            _iconHandle = Addressables.LoadAssetAsync<Sprite>(_currency.Icon);
            _icon.sprite = _iconHandle.WaitForCompletion();
            _icon.enabled = true;

            PlayerInventoryBroadcaster.Instance.Subscribe(this);
            PlayerInventoryBroadcaster.Instance.Broadcast(new PlayerCurrencyUpdateRequestMessage(_currency));
        }

        private void OnDestroy()
        {
            if (_iconHandle.IsValid()) Addressables.Release(_iconHandle);

            PlayerInventoryBroadcaster.Instance.Unsubscribe(this);
        }

        public void UpdateAmount(CurrencyItem currency, int amount)
        {
            if (currency.Id != _currency.Id) return;
            _amount.text = amount.ToString();
        }

        public void OnMessageReceived(PlayerCurrencyUpdateMessage message)
        {
            UpdateAmount(message.Currency, message.CurrentAmount);
        }
    }
}