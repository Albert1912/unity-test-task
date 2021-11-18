using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;

namespace Ui
{
    public class CardViewModelCollection : MonoBehaviour
    {
        [SerializeField] private List<CardViewModel> _cardViewModels;

        public List<IDownloadedImageConsumer> Consumers { get; private set; }
        
        public void ResetCollectionToDefault()
        {
            foreach (var cardViewModel in _cardViewModels)
            {
                cardViewModel.ResetToDefault();
            }
            
            Consumers = _cardViewModels.Select(x => x.Consumer).ToList();
        }
    }
}