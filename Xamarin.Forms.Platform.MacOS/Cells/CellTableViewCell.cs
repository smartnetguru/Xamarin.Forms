﻿using System;
using System.ComponentModel;
using AppKit;
using CoreGraphics;

namespace Xamarin.Forms.Platform.MacOS
{
	public class CellTableViewCell : FormsNSView, INativeElementView
	{
		static NSColor _defaultBackgroundFieldColor = NSColor.Clear;
		Cell _cell;
		NSTableViewCellStyle _style;

		public Action<object, PropertyChangedEventArgs> PropertyChanged;

		public CellTableViewCell(NSTableViewCellStyle style, string key)
		{
			_style = style;
			CreateUI();
		}

		public override void Layout()
		{
			var padding = 10;
			var availableHeight = Frame.Height;
			var availableWidth = Frame.Width - padding * 2;
			nfloat imageWidth = 0;
			nfloat imageHeight = 0;

			if (ImageView != null)
			{
				imageHeight = imageWidth = availableHeight;
				ImageView.Frame = new CGRect(padding, 0, imageWidth, imageHeight);
			}

			var labelHeights = availableHeight;
			var labelWidth = availableWidth - imageWidth;

			if (DetailTextLabel != null && !string.IsNullOrEmpty(DetailTextLabel.StringValue))
			{
				labelHeights = availableHeight / 2;
				DetailTextLabel.CenterTextVertically(new CGRect(imageWidth + padding, 0, labelWidth, labelHeights));
			}

			TextLabel.CenterTextVertically(new CGRect(imageWidth + padding, availableHeight - labelHeights, labelWidth, labelHeights));
			base.Layout();
		}

		public Cell Cell
		{
			get { return _cell; }
			set
			{
				if (_cell == value)
					return;

				ICellController cellController = _cell;

				if (cellController != null)
					Device.BeginInvokeOnMainThread(cellController.SendDisappearing);

				_cell = value;
				cellController = value;

				if (cellController != null)
					Device.BeginInvokeOnMainThread(cellController.SendAppearing);
			}
		}

		public Element Element => Cell;

		public void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, e);
		}

		public NSTextField TextLabel { get; private set; }

		public NSTextField DetailTextLabel { get; private set; }

		public NSImageView ImageView { get; private set; }

		internal static NSView GetNativeCell(NSTableView tableView, Cell cell, bool recycleCells = false, string templateId = "")
		{
			var id = cell.GetType().FullName;
			var renderer = (CellRenderer)Registrar.Registered.GetHandler(cell.GetType());
			var nativeCell = renderer.GetCell(cell, null, tableView);
			return nativeCell;
		}

		void CreateUI()
		{
			var style = _style;

			AddSubview(TextLabel = new NSTextField
			{
				Bordered = false,
				Selectable = false,
				Editable = false,
				Font = NSFont.LabelFontOfSize(NSFont.SystemFontSize)
			});

			TextLabel.Cell.BackgroundColor = NSColor.Clear;

			if (style == NSTableViewCellStyle.Image || style == NSTableViewCellStyle.Subtitle)
			{
				AddSubview(DetailTextLabel = new NSTextField
				{
					Bordered = false,
					Selectable = false,
					Editable = false,
					Font = NSFont.LabelFontOfSize(NSFont.SmallSystemFontSize)
				});
				DetailTextLabel.Cell.BackgroundColor = NSColor.Clear;
			}

			if (style == NSTableViewCellStyle.Image)
				AddSubview(ImageView = new NSImageView());

		}
	}
}