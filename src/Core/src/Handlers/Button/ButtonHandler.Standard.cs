using System;

namespace Microsoft.Maui.Handlers
{
	public sealed partial class ButtonMapper
	{
		public static void MapPadding(IButtonHandler handler, IButton button) { }
		public static void MapImageSource(IButtonHandler handler, IButton image) { }
	}

	public sealed partial class ButtonHandler : ViewHandler<IButton, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}