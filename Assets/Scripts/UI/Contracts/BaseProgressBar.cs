using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    /// <summary>
    /// A base class for progress bars.
    /// </summary>
    public abstract class BaseProgressBar : MonoBehaviour
    {
        #region Fields

        [SerializeField] protected Slider progressSlider;

        [SerializeField] protected Text currentProgressLabel;

        [SerializeField] protected Text maxProgressLabel;

        private Tween _sliderProgressTween;

        private Tween _progressTextTween;

        private Tween _maxProgressTextTween;

        #endregion

        #region Methods

        public void OnDestroy()
        {
            _sliderProgressTween?.Kill();
            _progressTextTween?.Kill();
            _maxProgressTextTween?.Kill();
        }

        /// <summary>
        /// Sets up the progress bar.
        /// </summary>
        /// <param name="max">The maximum value of the progress bar.</param>
        /// <param name="current">The current value of the progress bar. Defaults to 0.</param>
        /// <param name="dontSetMaxProgressText">Whether to skip setting the maximum progress text. Defaults to false.</param>
        public virtual void Setup(float max, float? current = 0, bool dontSetMaxProgressText = false)
        {
            if(progressSlider != null)
                progressSlider.maxValue = max;
            
            if(current.HasValue)
                SetProgress(current.Value);

            if(current.HasValue)
                SetCurrentProgressText(current.Value);

            if(!dontSetMaxProgressText)
                SetMaxProgressText(max);
        }

        /// <summary>
        /// Sets the current value of the progress bar without animating it.
        /// </summary>
        public virtual void SetProgress(float newValue)
        {
            if(progressSlider != null)
                progressSlider.value = newValue;
        }

        /// <summary>
        /// Sets the current value of the progress bar by animating it.
        /// </summary>
        public virtual void AnimateProgress(float newValue, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            if (progressSlider == null)
                return;

            _sliderProgressTween?.Kill();
            _sliderProgressTween = DOTween.To(() => progressSlider.value, x => progressSlider.value = x, newValue, duration).SetEase(ease);
        }

        /// <summary>
        /// Sets the current progress text without animating it.
        /// </summary>
        public void SetCurrentProgressText(float newValue)
        {
            if(currentProgressLabel != null)
                currentProgressLabel.text = newValue.ToString();
        }

        /// <summary>
        /// Sets the current progress text by animating it.
        /// </summary>
        public void AnimateCurrentProgressText(float newValue, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            if (currentProgressLabel == null)
                return;
            
            float.TryParse(currentProgressLabel.text, out float startValue);

            _progressTextTween?.Kill();
            _progressTextTween = DOTween.To(() => startValue, x => currentProgressLabel.text = x.ToString(), newValue, duration).SetEase(ease);
        }

        /// <summary>
        /// Sets the current progress text without animating it.
        /// </summary>
        public void SetMaxProgressText(float newValue)
        {
            if(maxProgressLabel != null)
                maxProgressLabel.text = newValue.ToString();
        }

        /// <summary>
        /// Sets the current progress text by animating it.
        /// </summary>
        public void AnimateMaxProgressText(float newValue, float duration = 0.5f, Ease ease = Ease.InOutSine)
        {
            if (maxProgressLabel == null)
                return;
            
            float.TryParse(maxProgressLabel.text, out float startValue);

            _maxProgressTextTween?.Kill();
            _maxProgressTextTween = DOTween.To(() => startValue, x => maxProgressLabel.text = x.ToString(), newValue, duration).SetEase(ease);
        }

        #endregion
    }
}