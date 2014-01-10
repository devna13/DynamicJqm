using System.Collections.Generic;
using System.Diagnostics;

namespace DynamicJqm
{
	[DebuggerDisplay("{EntityId} - {MobileInputType} - {Value}")]
	public class UiElementViewModel
	{

		//forgetting to set the type will be default to 0 element in the enum. 
		//always use overload
		//public UiElementViewModel()
		//{
		//}

		public UiElementViewModel(MobileInputType type)
		{
			MobileInputType = type;

			if (type == MobileInputType.CollapsibleSet
			    || type == MobileInputType.Collapsible
			    || type == MobileInputType.DropDown)
			{
				Childs = new List<UiElementViewModel>();
			}
		}

		/// <summary>
		/// Really I need to explain this? 
		/// </summary>
		public MobileInputType MobileInputType { get; set; }

		/// <summary>
		/// this will be used to pass question/questionItem Id to the ui and submitted back during form submittion as ID,Value pair
		/// </summary>
		public int EntityId { get; set; }

		//will be generated dynamically based on type and entityid
		//public string HtmlId { get; set; }//will be used for id & name attributes, will be passedback as the element gets submited

		//public string Text { get; set; }

		/// <summary>
		/// Left side label that explains the input
		/// </summary>
		public string Label { get; set; }

		public bool IsRequired { get; set; }
		public bool IsReadOnly { get; set; }
		public bool IsDisabled { get; set; }

		/// <summary>
		/// used for Options to be marked as selected
		/// </summary>
		public bool IsSelected { get; set; }


		/// <summary>
		/// string representation of an entities value
		/// </summary>
		public string Value { get; set; }


		/// <summary>
		/// only for collapsible & collapsible set & dropdown
		/// </summary>
		public IList<UiElementViewModel> Childs { get; set; }


		/// <summary>
		/// Not being used yet
		/// </summary>
		public string Tooltip { get; set; }
	}
}