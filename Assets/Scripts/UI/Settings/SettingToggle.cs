using DG.Tweening;
using UnityEngine;

namespace UI.Settings
{
    public class SettingToggle : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private RectTransform _settingIcon;
        [SerializeField] private float rotationDuration = 1.0f;

        private bool isWindowOpen = false;

        [Header("Music Settings")] [SerializeField]
        private AudioSource _audioSource = null;

        [SerializeField] private float _audioVolume = 0.5f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private GameObject _volumeIcon;

        private void Start()
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Load audio volume
            _audioVolume = PlayerPrefs.GetFloat("AudioVolume", 0.5f);
            _audioSource.volume = _audioVolume;
            _volumeIcon.SetActive(true);
        }

        private void SaveSettings()
        {
            // Save audio volume
            PlayerPrefs.SetFloat("AudioVolume", _audioVolume);

            PlayerPrefs.Save();
        }

        public void ToggleWindow()
        {
            Vector3 targetRotation = _settingIcon.eulerAngles + new Vector3(0, 0, 1) * 90;

            _settingIcon.DORotate(targetRotation, rotationDuration, RotateMode.FastBeyond360);
            isWindowOpen = !isWindowOpen;
            _window.SetActive(isWindowOpen);
            SaveSettings();
        }

        public void ToggleMusic()
        {
            if (_audioSource.volume > 0.0f)
            {
                _audioSource.DOFade(0.0f, _duration).OnComplete(() => _audioSource.Pause());
                _volumeIcon.SetActive(false);
            }
            else
            {
                _audioSource.UnPause();
                _audioSource.DOFade(_audioVolume, _duration);
                _volumeIcon.SetActive(true);
            }

            SaveSettings();
        }
    }
}