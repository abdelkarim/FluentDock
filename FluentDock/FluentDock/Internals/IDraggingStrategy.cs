using System.Windows.Controls.Primitives;
using FluentDock.Primitives;

namespace FluentDock.Internals
{
    internal interface IDraggingStrategy
    {
        void ComputeMinMax(SplitterGrip grip, out double min, out double max);
        void OnDragStarted(SplitterGrip splitterGrip, DragStartedEventArgs args);
        void OnDragDelta(SplitterGrip grip, DragDeltaEventArgs args);
        void OnDragCompleted(SplitterGrip grip, DragCompletedEventArgs e);
        void Cancel();
        void Move(double horizontalChange, double verticalChange);
    }
}
