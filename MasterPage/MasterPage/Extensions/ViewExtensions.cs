namespace MasterPage.Extensions
{
    public static class ViewExtensions
    {
        public static Rect GetBoundsRelativeTo(this VisualElement view, VisualElement relativeTo)
        {
            var absBounds = view.GetBounds();
            var relativeBounds = relativeTo.GetBounds();
            return new Rect(absBounds.X - relativeBounds.X, absBounds.Y - relativeBounds.Y, absBounds.Width, absBounds.Height);
        }

        public static Rect GetBounds(this VisualElement view)
        {
            var point = view.GetAbsolutePosition();
            return new Rect(point.X, point.Y, view.Width, view.Height);
        }

        public static Point GetAbsolutePosition(this VisualElement view)
        {
            var location = new Point(view.X, view.Y);
            var parent = view.Parent as VisualElement;

            while (parent != null)
            {
                location = new Point(location.X + parent.X, location.Y + parent.Y);
                parent = parent.Parent as VisualElement;
            }

            return location;
        }
    }
}
