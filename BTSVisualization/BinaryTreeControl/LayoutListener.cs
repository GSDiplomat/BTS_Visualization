using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BTSVisualization
{
    public class LayoutChangeEventArgs : EventArgs
    {
        public readonly Rect Rect;
        public LayoutChangeEventArgs(Rect rect) => Rect = rect;
    }

    public class LayoutListener
    {
        public void ChangeTarget(UIElement target, UIElement origin = null)
        {
            if (this.target != null)
                this.target.LayoutUpdated -= OnLayoutUpdate;

            this.target = target;
            this.origin = origin;
            OnLayoutUpdate(null, null);

            if (this.target != null)
                target.LayoutUpdated += OnLayoutUpdate;
        }

        void OnLayoutUpdate(object sender, EventArgs e)
        {
            var newRenderRect = GetRenderRect();
            if (newRenderRect != currRenderRect)
            {
                currRenderRect = newRenderRect;
                FireChanged();
            }
        }

        UIElement target, origin;
        Rect currRenderRect = Rect.Empty;

        public static Rect ComputeRenderRect(UIElement target, UIElement origin) =>
            new Rect(target.TranslatePoint(new Point(), origin), target.RenderSize);

        Rect GetRenderRect() => ComputeRenderRect(target, origin);

        void FireChanged() =>
            Changed?.Invoke(target, new LayoutChangeEventArgs(currRenderRect));

        public event EventHandler<LayoutChangeEventArgs> Changed;
    }
}
