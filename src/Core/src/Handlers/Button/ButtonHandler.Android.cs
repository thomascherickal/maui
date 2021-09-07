using Android.Content.Res;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using Google.Android.Material.Button;
using Microsoft.Maui.Graphics;
using AView = Android.Views.View;

namespace Microsoft.Maui.Handlers
{
	public partial interface IButtonHandler : IElementHandler
	{
		new AView? NativeView { get; }

		// These are a little odd here but Android doesn't let you retrieve these from a control
		ButtonHandler.ButtonClickListener ClickListener { get; }
		ButtonHandler.ButtonTouchListener TouchListener { get; }
	}

	public sealed partial class ButtonMapper
	{
		// This is a Android-specific mapping
		public static void MapBackground(IButtonHandler handler, IButton button)
		{
			handler.NativeView?.UpdateBackground(button);
		}

		public static void MapPadding(IButtonHandler handler, IButton button)
		{
			if (handler.NativeView is AppCompatButton compatButton)
				compatButton.UpdatePadding(button);
		}

		public static void MapDisconnectHandler(IButtonHandler arg1, IButton arg2, object? arg3)
		{
			arg1.ClickListener.Handler = null;
			arg1.NativeView?.SetOnClickListener(null);

			arg1.TouchListener.Handler = null;
			arg1.NativeView?.SetOnTouchListener(null);

			// These should get wired up differently via append/prepend extensions
			ViewHandler.MapDisconnectHandler((IViewHandler)arg1, arg2, arg3);
		}

		public static void MapConnectHandler(IButtonHandler arg1, IButton arg2, object? arg3)
		{
			arg1.ClickListener.Handler = arg1;
			arg1.NativeView?.SetOnClickListener(arg1.ClickListener);

			arg1.TouchListener.Handler = arg1;
			arg1.NativeView?.SetOnTouchListener(arg1.TouchListener);


			// These should get wired up differently via append/prepend extensions
			ViewHandler.MapConnectHandler(arg1, arg2, arg3);
		}
	}

	public sealed partial class ButtonHandler : ViewHandler<IButton, MaterialButton>, IButtonHandler
	{
		// Move these to Factory Methods?
		ButtonClickListener IButtonHandler.ClickListener { get; } = new ButtonClickListener();
		ButtonTouchListener IButtonHandler.TouchListener { get; } = new ButtonTouchListener();

		//ImageSourcePartWrapper<ButtonHandler>? _imageSourcePartWrapper;

		//ImageSourcePartWrapper<ButtonHandler> ImageSourcePartWrapper =>
		//	_imageSourcePartWrapper ??= new ImageSourcePartWrapper<ButtonHandler>(
		//		this, (h) => h.VirtualView.ImageSource, null, null, OnSetImageSourceDrawable);

		static ColorStateList? _transparentColorStateList;

		protected override MaterialButton CreateNativeView()
		{
			MaterialButton nativeButton = new MauiMaterialButton(Context)
			{
				IconGravity = MaterialButton.IconGravityTextStart,
				IconTintMode = Android.Graphics.PorterDuff.Mode.Add,
				IconTint = (_transparentColorStateList ??= Colors.Transparent.ToDefaultColorStateList()),
				SoundEffectsEnabled = false
			};

			return nativeButton;
		}

		bool NeedsExactMeasure()
		{
			if (VirtualView.VerticalLayoutAlignment != Primitives.LayoutAlignment.Fill
				&& VirtualView.HorizontalLayoutAlignment != Primitives.LayoutAlignment.Fill)
			{
				// Layout Alignments of Start, Center, and End will be laying out the TextView at its measured size,
				// so we won't need another pass with MeasureSpecMode.Exactly
				return false;
			}

			if (VirtualView.Width >= 0 && VirtualView.Height >= 0)
			{
				// If the Width and Height are both explicit, then we've already done MeasureSpecMode.Exactly in 
				// both dimensions; no need to do it again
				return false;
			}

			// We're going to need a second measurement pass so TextView can properly handle alignments
			return true;
		}

		//TODO MOVE TO COMMAND MAPPER
		public override void NativeArrange(Rectangle frame)
		{
			var nativeView = this.GetWrappedNativeView();

			if (nativeView == null || Context == null)
			{
				return;
			}

			if (frame.Width < 0 || frame.Height < 0)
			{
				return;
			}

			// Depending on our layout situation, the TextView may need an additional measurement pass at the final size
			// in order to properly handle any TextAlignment properties.
			if (NeedsExactMeasure())
			{
				nativeView.Measure(MakeMeasureSpecExact(frame.Width), MakeMeasureSpecExact(frame.Height));
			}

			base.NativeArrange(frame);
		}

		int MakeMeasureSpecExact(double size)
		{
			// Convert to a native size to create the spec for measuring
			var deviceSize = (int)Context!.ToPixels(size);
			return MeasureSpecMode.Exactly.MakeMeasureSpec(deviceSize);
		}

		//void OnSetImageSourceDrawable(Drawable? obj)
		//{
		//	NativeView.Icon = obj;
		//	VirtualView?.ImageSourceLoaded();
		//}

		TextView? ITextHandler.NativeView => this.NativeView;

		AView? IButtonHandler.NativeView => this.NativeView;

		public class ButtonClickListener : Java.Lang.Object, AView.IOnClickListener
		{
			public IButtonHandler? Handler { get; set; }

			public virtual void OnClick(AView? v)
			{
				Handler?.VirtualView?.Clicked();
			}
		}

		public class ButtonTouchListener : Java.Lang.Object, AView.IOnTouchListener
		{
			public IButtonHandler? Handler { get; set; }

			public virtual bool OnTouch(AView? v, global::Android.Views.MotionEvent? e)
			{
				var button = Handler?.VirtualView;
				switch (e?.ActionMasked)
				{
					case MotionEventActions.Down:
						button?.Pressed();
						break;
					case MotionEventActions.Up:
						button?.Released();
						break;
				}

				return false;
			}
		}
	}
}