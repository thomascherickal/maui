
using System;

namespace Microsoft.Maui.Handlers
{
	public partial interface IButtonHandler : IViewHandler
	{
		public new IButton VirtualView { get; }
	}	

	public sealed partial class ButtonMapper
	{
		public static IPropertyMapper<IButton, IButtonHandler> Mapper =
			new PropertyMapper<IButton, IButtonHandler>(ViewHandler.ViewMapper, TextMapper.Mapper)
			{
#if __ANDROID__
				[nameof(IButton.Background)] = MapBackground,
				[nameof(IButton.Padding)] = MapPadding
#endif
			};

		public static CommandMapper<IButton, IButtonHandler> CommandMapper = new(ViewHandler.ViewCommandMapper)
		{
#if __ANDROID__
			[nameof(IViewHandler.ConnectHandler)] = MapConnectHandler,
			[nameof(IViewHandler.DisconnectHandler)] = MapDisconnectHandler
#endif
		};
	}

	public sealed partial class ButtonHandler : IButtonHandler, ITextHandler
	{

		public ButtonHandler() : base(ButtonMapper.Mapper)
		{

		}

		public ButtonHandler(IPropertyMapper? mapper = null) : base(mapper ?? ButtonMapper.Mapper)
		{
		}
	}
}
