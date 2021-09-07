namespace Microsoft.Maui.Handlers
{
	public partial interface ITextHandler
	{
	}


	public sealed partial class TextMapper
	{
		public static IPropertyMapper<IText, ITextHandler> Mapper = new PropertyMapper<IText, ITextHandler>()
		{
			[nameof(IText.CharacterSpacing)] = MapCharacterSpacing,
			[nameof(IText.Font)] = MapFont,
			[nameof(IText.Text)] = MapText,
			[nameof(IText.TextColor)] = MapTextColor,
		};
	}
}
