using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Skyscraper.Views.Controls
{
    public class ClippingBorder : Border
    {
        private RectangleGeometry clippingRectangle = new RectangleGeometry();
        private object previousClippingRectangle;

        public override UIElement Child
        {
            get
            {
                return base.Child;
            }
            set
            {
                if (this.Child != value)
                {
                    if (this.Child != null)
                    {
                        this.Child.SetValue(UIElement.ClipProperty, this.previousClippingRectangle);
                    }

                    if (value != null)
                    {
                        this.previousClippingRectangle = value.ReadLocalValue(UIElement.ClipProperty);
                    }
                    else
                    {
                        this.previousClippingRectangle = null;
                    }

                    base.Child = value;
                }
            }
        }

        protected override void OnRender(DrawingContext dc)
        {
            this.OnApplyChildClip();

            base.OnRender(dc);
        }

        protected virtual void OnApplyChildClip()
        {
            UIElement child = this.Child;

            if (child != null)
            {
                this.clippingRectangle.RadiusX = this.clippingRectangle.RadiusY = Math.Max(0.0, this.CornerRadius.TopLeft - (this.BorderThickness.Left * 0.5));
                this.clippingRectangle.Rect = new Rect(child.RenderSize);
                child.Clip = this.clippingRectangle;
            }
        }
    }
}