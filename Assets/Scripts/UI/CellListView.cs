using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    [Serializable]
    public class CellListView<TCell, TData> where TCell : Object
    {
        [SerializeField] private TCell _cellPrefab;
        [SerializeField] private RectTransform _cellsParent;

        private readonly Dictionary<TCell, TData> _cellDataMap = new();

        public IReadOnlyDictionary<TCell, TData> CellDataMap => _cellDataMap;

        public event Action<TData> OnSelectCell;

        public void PopulateList(IEnumerable<TData> items, Action<TCell, TData, Action<TCell>> setupCellDelegate)
        {
            for (int i = _cellsParent.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(_cellsParent.GetChild(i).gameObject);
            }

            _cellDataMap.Clear();

            foreach (TData data in items)
            {
                TCell cell = Object.Instantiate(_cellPrefab, _cellsParent);
                setupCellDelegate?.Invoke(cell, data, OnItemCellSelected);

                _cellDataMap[cell] = data;
            }
        }

        private void OnItemCellSelected(TCell cell)
        {
            OnSelectCell?.Invoke(_cellDataMap[cell]);
        }
    }
}