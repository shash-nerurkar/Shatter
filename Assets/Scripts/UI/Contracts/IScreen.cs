using System;

namespace Scripts.UI
{
    /// <summary>
    /// Represents a UI screen.
    /// </summary>
    public interface IScreen
    {
        public void Show(Action onHidden = null);

        public void Hide();
    }
}
