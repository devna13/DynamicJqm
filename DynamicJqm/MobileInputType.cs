namespace DynamicJqm
{
	public enum MobileInputType
	{

		/// <summary>
		/// Just some informational text  in span or p tag, readonly
		/// </summary>
		Note,

		TextBox,
		/// <summary>
		/// [0-9]*
		/// </summary>
		NumericText,
		//CurrencyText, use the textbox and pass value use string format {"c"}. adding $ messes up the layout


		/// <summary>
		/// 40 * 8         col * rows text area.
		/// </summary>
		TextArea,


		/// <summary>
		/// DateFormat = "MM/dd/yyyy"; 
		/// </summary>
		DateText,
		/// <summary>
		///  DateTimeFormat = "yyyy-MM-dd'T'HH:mm:ss";//2000-01-01T00:00:00 , Must be converted into UTC 00:00 before passing to UI
		/// </summary>
		DateTimeText,
		/// <summary>
		/// TimeFormat = "HH:mm:ss";//00:00:00, Must be converted into UTC 00:00 before passing to UI
		/// </summary>
		TimeText,


		/// <summary>
		/// flip switch
		/// </summary>
		YesNo, //desktop rendering problem if we remove swipe event in JS then we can start rendering this

		/// <summary>
		/// Checkbox
		/// </summary>
		CheckBox,

		/// <summary>
		/// select
		/// </summary>
		DropDown,
		/// <summary>
		/// option tag (to be used for Html select options )
		/// </summary>
		DropDownOption,



		/// <summary>
		/// contains list of collapsibles, do not set the entity id
		/// </summary>
		CollapsibleSet,
		/// <summary>
		/// contains other uielements, do not set the entity id
		/// </summary>
		Collapsible,

		/// <summary>
		/// Readonly ListView rendered as Ul li. just fill 
		/// </summary>
		UnSortedList,//populate the childs as li
		SortedList,//populate the childs as li
		Li,


		/// <summary>
		/// checkbox group --TODO: Implement future- nov 25 
		/// </summary>
		//MultiSelect
	}
}