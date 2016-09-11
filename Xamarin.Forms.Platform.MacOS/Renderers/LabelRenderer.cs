﻿using System;
using AppKit;

using RectangleF = CoreGraphics.CGRect;
using SizeF = CoreGraphics.CGSize;
using PointF = CoreGraphics.CGPoint;
using System.ComponentModel;

namespace Xamarin.Forms.Platform.MacOS
{
	public class LabelRenderer : ViewRenderer<Label, NSTextField>
	{
		SizeRequest _perfectSize;

		bool _perfectSizeValid;

		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (!_perfectSizeValid)
			{
				_perfectSize = base.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);
				_perfectSize.Minimum = new Size(Math.Min(10, _perfectSize.Request.Width), _perfectSize.Request.Height);
				_perfectSizeValid = true;
			}

			if (widthConstraint >= _perfectSize.Request.Width && heightConstraint >= _perfectSize.Request.Height)
				return _perfectSize;

			var result = base.GetDesiredSize(widthConstraint, heightConstraint);
			result.Minimum = new Size(Math.Min(10, result.Request.Width), result.Request.Height);
			if ((Element.LineBreakMode & (LineBreakMode.TailTruncation | LineBreakMode.HeadTruncation | LineBreakMode.MiddleTruncation)) != 0)
			{
				if (result.Request.Width > widthConstraint)
					result.Request = new Size(Math.Max(result.Minimum.Width, widthConstraint), result.Request.Height);
			}

			return result;
		}

		public override void Layout()
		{
			base.Layout();
			LayoutSubviews();
		}

		void LayoutSubviews()
		{
			if (Control == null)
				return;

			if (Bounds.Height < 0)
				return;

			SizeF fitSize;
			nfloat labelHeight;
			switch (Element.VerticalTextAlignment)
			{
				case TextAlignment.Start:
					fitSize = Control.SizeThatFits(Element.Bounds.Size.ToSizeF());
					labelHeight = (nfloat)Math.Min(Bounds.Height, fitSize.Height);
					Control.Frame = new RectangleF(0, Bounds.Height - labelHeight, (nfloat)Element.Width, labelHeight);
					break;
				case TextAlignment.Center:
					Control.Frame = new RectangleF(0, Bounds.Height - Element.Height, (nfloat)Element.Width, (nfloat)Element.Height);
					break;
				case TextAlignment.End:
					nfloat yOffset = 0;
					fitSize = Control.SizeThatFits(Element.Bounds.Size.ToSizeF());
					labelHeight = (nfloat)Math.Min(Bounds.Height, fitSize.Height);
					yOffset = (nfloat)labelHeight;
					Control.Frame = new RectangleF(0, 0, (nfloat)Element.Width, labelHeight);
					break;
			}
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			if (e.NewElement != null)
			{
				if (Control == null)
				{
					SetNativeControl(new NSTextField() { BackgroundColor = NSColor.Clear, Editable = false, Bezeled = false, DrawsBackground = false });
				}

				UpdateText();
				UpdateLineBreakMode();
				UpdateAlignment();
			}

			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (e.PropertyName == Label.HorizontalTextAlignmentProperty.PropertyName)
				UpdateAlignment();
			else if (e.PropertyName == Label.VerticalTextAlignmentProperty.PropertyName)
				LayoutSubviews();
			else if (e.PropertyName == Label.TextColorProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == Label.FontProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == Label.TextProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == Label.FormattedTextProperty.PropertyName)
				UpdateText();
			else if (e.PropertyName == Label.LineBreakModeProperty.PropertyName)
				UpdateLineBreakMode();
		}

		protected override void SetBackgroundColor(Color color)
		{
			if (color == Color.Default)
				BackgroundColor = NSColor.Clear;
			else
				BackgroundColor = color.ToNSColor();
		}

		void UpdateAlignment()
		{
			Control.Alignment = Element.HorizontalTextAlignment.ToNativeTextAlignment();
		}

		void UpdateLineBreakMode()
		{
			_perfectSizeValid = false;

			switch (Element.LineBreakMode)
			{
				case LineBreakMode.NoWrap:
					Control.LineBreakMode = NSLineBreakMode.Clipping;
					Control.MaximumNumberOfLines = 1;
					break;
				case LineBreakMode.WordWrap:
					Control.LineBreakMode = NSLineBreakMode.ByWordWrapping;
					Control.MaximumNumberOfLines = 0;
					break;
				case LineBreakMode.CharacterWrap:
					Control.LineBreakMode = NSLineBreakMode.CharWrapping;
					Control.MaximumNumberOfLines = 0;
					break;
				case LineBreakMode.HeadTruncation:
					Control.LineBreakMode = NSLineBreakMode.TruncatingHead;
					Control.MaximumNumberOfLines = 1;
					break;
				case LineBreakMode.MiddleTruncation:
					Control.LineBreakMode = NSLineBreakMode.TruncatingMiddle;
					Control.MaximumNumberOfLines = 1;
					break;
				case LineBreakMode.TailTruncation:
					Control.LineBreakMode = NSLineBreakMode.TruncatingTail;
					Control.MaximumNumberOfLines = 1;
					break;
			}
		}

		void UpdateText()
		{
			_perfectSizeValid = false;

			var values = Element.GetValues(Label.FormattedTextProperty, Label.TextProperty, Label.TextColorProperty);
			var formatted = (FormattedString)values[0];
			if (formatted != null)
				Control.AttributedStringValue = formatted.ToAttributed(Element, (Color)values[2]);
			else
			{
				Control.StringValue = (string)values[1] ?? "";
				// default value of color documented to be black in iOS docs
				Control.Font = Element.ToNSFont();
				Control.TextColor = ((Color)values[2]).ToUIColor(ColorExtensions.Black);
			}

			LayoutSubviews();
		}
	}
}

