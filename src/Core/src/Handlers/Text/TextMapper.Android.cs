using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Widget;
using Google.Android.Material.Button;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Graphics;
using static Microsoft.Maui.Handlers.ButtonHandler;
using AView = Android.Views.View;

namespace Microsoft.Maui.Handlers
{
	public partial interface ITextHandler : IViewHandler
	{
		new TextView? NativeView { get; }
	}

	public sealed partial class TextMapper
	{
		public static void MapTextColor(ITextHandler handler, IText text)
		{
			handler.NativeView?.UpdateTextColor(text);
		}

		public static void MapCharacterSpacing(ITextHandler handler, IText text)
		{
			handler.NativeView?.UpdateCharacterSpacing(text);
		}

		public static void MapFont(ITextHandler handler, IText text)
		{
			var fontManager = handler.GetRequiredService<IFontManager>();
			handler.NativeView?.UpdateFont(text, fontManager);
		}

		public static void MapText(ITextHandler handler, IText text)
		{			
			handler.NativeView?.UpdateTextPlainText(text);
		}
	}
}