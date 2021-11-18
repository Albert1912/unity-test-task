using System;
using Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class CardViewModel : MonoBehaviour
    {
        [SerializeField] private RawImage _image;
        public IDownloadedImageConsumer Consumer { get; private set; }

        private void Awake()
        {
            if (_image == null)
            {
                throw new ArgumentNullException(nameof(_image));
            }

            Consumer = new DownloadedImageConsumer(this);
        }

        public void SetImage(Texture image)
        {
            if (image == null)
            {
                return;
            }

            _image.texture = image;
        }

        public void ResetToDefault()
        {
            if (_image.texture != null)
            {
                Destroy(_image.texture);
            }

            _image.texture = null;
        }
    }
}