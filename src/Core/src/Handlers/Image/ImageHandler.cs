#nullable enable
using System;
using System.Threading;

namespace Microsoft.Maui.Handlers
{
	public partial interface IImageHandler : IViewHandler
	{
		new IImage VirtualView { get; }
		ImageSourceServiceResultManager SourceManager { get; }
	}

	public static partial class ImageMapper
	{
		public static IPropertyMapper<IImage, IImageHandler> Mapper = new PropertyMapper<IImage, IImageHandler>(ViewHandler.ViewMapper)
		{
#if __ANDROID__
			[nameof(IImage.Background)] = MapBackground,
#endif
			[nameof(IImage.Aspect)] = MapAspect,
			[nameof(IImage.IsAnimationPlaying)] = MapIsAnimationPlaying,
			[nameof(IImage.Source)] = MapSource,
		};

		public static CommandMapper<IImage, IButtonHandler> ImageButtonCommandMapper = new(ButtonMapper.CommandMapper)
		{
		};
	}

	public partial class ImageHandler
	{
		public ImageSourceServiceResultManager SourceManager { get; } = new ImageSourceServiceResultManager();

		public ImageHandler() : base(ImageMapper.Mapper)
		{
		}

		public ImageHandler(IPropertyMapper mapper) : base(mapper ?? ImageMapper.Mapper)
		{
		}
	}
}