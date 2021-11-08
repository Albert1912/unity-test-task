using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Ui
{
    public class CardViewModelCollection : MonoBehaviour
    {
        [SerializeField] private List<CardViewModel> _cardViewModels;

        public void ResetCollectionToDefault()
        {
            foreach (var cardViewModel in _cardViewModels)
            {
                cardViewModel.ResetToDefault();
            }
        }

    public List<IDownloadedImageConsumer> GetConsumers()
        {
            return _cardViewModels.Select(x => x.Consumer).ToList();
        }
    }
}