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

        #endregion

        #region Methods

        /// <summary>
        /// Sets up the progress bar.
        /// </summary>
        /// <param name="max">The maximum value of the progress bar.</param>
        /// <param name="current">The current value of the progress bar. Defaults to 0.</param>
        /// <param name="dontSetProgress">Whether to skip setting the progress. Defaults to false.</param>
        /// <param name="dontSetCurrentProgressText">Whether to skip setting the current progress text. Defaults to false.</param>
        /// <param name="dontSetMaxProgressText">Whether to skip setting the maximum progress text. Defaults to false.</param>
        public virtual void Setup(float max, float current = 0, bool dontSetProgress = false, bool dontSetCurrentProgressText = false, bool dontSetMaxProgressText = false)
        {
            if(progressSlider != null)
                progressSlider.maxValue = max;
            
            if(!dontSetProgress)
                SetProgress(current);

            if(!dontSetCurrentProgressText)
                SetCurrentProgressText(current);

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
            if(progressSlider != null)
                DOTween.To(() => progressSlider.value, x => progressSlider.value = x, newValue, duration).SetEase(ease);
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
            if(currentProgressLabel != null)
            {
                float currentValue = 0;
                if (float.TryParse(currentProgressLabel.text, out float parsedValue))
                {
                    currentValue = parsedValue;
                }

                DOTween.To(() => currentValue, x => currentProgressLabel.text = x.ToString(), newValue, duration).SetEase(ease);
            }
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
            if(maxProgressLabel != null)
            {
                float currentValue = 0;
                if (float.TryParse(maxProgressLabel.text, out float parsedValue))
                {
                    currentValue = parsedValue;
                }

                DOTween.To(() => currentValue, x => maxProgressLabel.text = x.ToString(), newValue, duration).SetEase(ease);
            }
        }

        #endregion
    }
}