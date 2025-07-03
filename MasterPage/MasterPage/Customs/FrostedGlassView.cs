using SkiaSharp;
using SkiaSharp.Views.Maui.Controls;

namespace MasterPage.Customs
{
    public class FrostedGlassView : SKCanvasView
    {
        public FrostedGlassView()
        {
            this.PaintSurface += FrostedGlassView_PaintSurface;
            this.IgnorePixelScaling = true;

            // Listen for theme changes
            Application.Current!.RequestedThemeChanged += FrostedGlassView_RequestedThemeChanged;
        }

        private void FrostedGlassView_RequestedThemeChanged(object? sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(InvalidateSurface);
        }

        private void FrostedGlassView_PaintSurface(object? sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;
            canvas.Clear();

            // Detect current theme
            var isDark = Application.Current?.RequestedTheme == AppTheme.Light;

            // Apply appropriate blur color
            var blurColor = isDark
                ? SKColors.Black.WithAlpha(100)
                : SKColors.White.WithAlpha(100);

            using var paint = new SKPaint
            {
                ImageFilter = SKImageFilter.CreateBlur(20f, 20f),
                Color = blurColor,
            };

            canvas.DrawRect(new SKRect(0, 0, info.Width, info.Height), paint);
        }
    }
}
