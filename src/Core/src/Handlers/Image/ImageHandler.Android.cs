#nullable enable
using System.Threading.Tasks;
using Android.Widget;
using AndroidX.AppCompat.Widget;

namespace Microsoft.Maui.Handlers
{
	public partial interface IImageHandler : IViewHandler
	{
		public new ImageView NativeView { get; }
	}

	public static partial class ImageMapper
	{

		public static void MapBackground(IImageHandler handler, IImage image)
		{
			handler.UpdateValue(nameof(IViewHandler.ContainerView));

			handler.GetWrappedNativeView()?.UpdateBackground(image);
		}

		public static void MapAspect(IImageHandler handler, IImage image) =>
			handler.NativeView?.UpdateAspect(image);

		public static void MapIsAnimationPlaying(IImageHandler handler, IImage image) =>
			handler.NativeView?.UpdateIsAnimationPlaying(image);

		public static void MapSource(IImageHandler handler, IImage image) =>
			MapSourceAsync(handler, image).FireAndForget(handler);

		public static async Task MapSourceAsync(IImageHandler handler, IImage image)
		{
			if (handler.NativeView == null)
				return;

			var token = handler.SourceManager.BeginLoad();

			var provider = handler.GetRequiredService<IImageSourceServiceProvider>();
			var result = await handler.NativeView.UpdateSourceAsync(image, provider, token);

			handler.SourceManager.CompleteLoad(result);
		}
	}

	public partial class ImageHandler : ViewHandler<IImage, ImageView>
	{
		protected override ImageView CreateNativeView() => new AppCompatImageView(Context);

		protected override void DisconnectHandler(ImageView nativeView)
		{
			base.DisconnectHandler(nativeView);

			SourceManager.Reset();
		}

		public override bool NeedsContainer =>
			VirtualView?.Background != null ||
			base.NeedsContainer;
	}
}