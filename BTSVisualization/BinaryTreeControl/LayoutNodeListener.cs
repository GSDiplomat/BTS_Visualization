using System;
using System.Collections.Generic;
using System.Windows;

namespace BTSVisualization
{
    public class LayoutNodeListener
    {
        public event EventHandler CoordinatesChanged;
        private List<FrameworkElement> ChildList = new List<FrameworkElement>();

        public void AddTarget(FrameworkElement target)
        {
            ChildList.Add(target);
        }

        public void RedrawChild()
        {
            foreach (var child in ChildList)
            {
                SetGridCoordinates(child);
            }
        }

        public void SetGridCoordinates(FrameworkElement child) => CoordinatesChanged?.Invoke(child, new EventArgs());
    }
}
