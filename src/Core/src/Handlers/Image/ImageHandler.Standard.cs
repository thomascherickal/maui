#nullable enable
using System;

namespace Microsoft.Maui.Handlers
{
	public static partial class ImageMapper
	{
		public static void MapAspect(IImageHandler handler, IImage image) { }
		public static void MapIsAnimationPlaying(IImageHandler handler, IImage image) { }
		public static void MapSource(IImageHandler handler, IImage image) { }

	}

	public partial class ImageHandler : ViewHandler<IImage, object>
	{
		protected override object CreateNativeView() => throw new NotImplementedException();
	}
}