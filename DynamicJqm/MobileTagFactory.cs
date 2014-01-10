using System;
using System.Globalization;
using System.Text;
using System.Web.Mvc;

namespace DynamicJqm
{
	public class MobileTagFactory
	{
		public static readonly CultureInfo Usa = new CultureInfo("en-US");

		#region Private

		private string CreateLabel(string label)
		{
			return "<label>{0}</label>".F(label);
		}

		private string CreateLiTag(ref UiElementViewModel li)
		{
			var elem = new TagBuilder("li");

			elem.InnerHtml = "<p>{0}</p>".F(li.Value);

			return elem.ToString();
		}

		private string CreateLiTagsFromChilds(ref UiElementViewModel element)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < element.Childs.Count; i++)
			{
				var li = element.Childs[i];
				sb.Append(CreateLiTag(ref li));
			}

			return sb.ToString();
		}

		private string CreateOptionTag(ref UiElementViewModel option)
		{
			var op = new TagBuilder("option");

			op.SetValue(ref option);
			//op.MergeAttribute("value", option.EntityId.ToString(Usa));

			op.SetInnerText(option.Label);
			op.SetDisplayOptions(ref option);
			return op.ToString();
		}

		private string CreateOptionTagsFromChilds(ref UiElementViewModel element)
		{
			var sb = new StringBuilder();
			for (int i = 0; i < element.Childs.Count; i++)
			{
				var op = element.Childs[i];
				sb.Append(CreateOptionTag(ref op));
			}

			return sb.ToString();
		}

		private string CreateYesNoOptions(bool yesSelected)
		{
			var yes = new UiElementViewModel(MobileInputType.DropDownOption)
			{
				Label = "Yes",
				Value = "1",
				IsSelected = yesSelected
			};
			var no = new UiElementViewModel(MobileInputType.DropDownOption)
			{
				Label = "No",
				Value = "0",
				IsSelected = !yesSelected
			};

			return CreateOptionTag(ref yes) + CreateOptionTag(ref no);

		}
		private string SetDisplayOptions(ref UiElementViewModel elem)
		{
			var sb = new StringBuilder();
			if (elem.IsReadOnly)
			{
				sb.Append("readonly=\"readonly\"");
			}

			if (elem.IsRequired)
			{

				sb.Append("required=\"required\"");
			}

			if (elem.IsSelected)
			{
				sb.Append("selected=\"selected\"");
			}

			if (elem.IsDisabled)
			{
				sb.Append("disabled=\"disabled\"");
			}

			return sb.ToString();
		}
		#endregion

		public string Create(UiElementViewModel element)
		{

			if (element == null)
			{
				throw new ArgumentNullException("element");
			}

			TagBuilder formElement;

			switch (element.MobileInputType)
			{
				case MobileInputType.Note:
				{
					formElement = new TagBuilder("strong");
					formElement.SetInnerText(element.Value);
					formElement.SetNameId(ref element);
					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.TextBox:
				{
					formElement = new TagBuilder("input");
					formElement.SetType("text");
					formElement.SetNameIdValue(ref element);
					formElement.AddClearBtn();
					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.NumericText:
				{
					formElement = new TagBuilder("input");
					formElement.SetType("number");
					formElement.SetNameIdValue(ref element);
					formElement.AddClearBtn();
					break;
				}
					////----------------------------------------------------------------
					//case MobileInputType.CurrencyText:
					//	{
					//		formElement = new TagBuilder("input");
					//		formElement.SetType("text");

					//		//*** manipulate the value ***


					//		formElement.SetNameIdValue(ref element);
					//		formElement.AddClearBtn();
					//		break;
					//	}
					//----------------------------------------------------------------
				case MobileInputType.CheckBox:
				{
					//Dear future me. Please forgive me. 
					//I can't even begin to express how sorry I am.

					//formElement = new TagBuilder("input");
					//formElement.SetType("checkbox");
					//formElement.SetNameId(ref element);
					//formElement.SetChecked( element.Value.CompareIgnoreCase("yes") );


					//This crap is impossible to be rendered.

					/*	
			<div class="ui-field-contain">
				<label for="actions"></label>
				<fieldset id="actions" data-role="controlgroup" data-type="vertical">
					<input type="checkbox" name="checkbox-0" id="checkbox-0">
					<label for="checkbox-0">Provider Selected</label>
				</fieldset>
			</div>*/
					var str = "{0}_{1}".F(element.MobileInputType.Str(), element.EntityId);
					var idStr = element.EntityId.ToString(Global.Usa);
					string checkboxHtml =
						"<div class=\"ui-field-contain\"><label for=\"{0}\">&nbsp;</label><fieldset id=\"{0}\" data-role=\"controlgroup\" data-type=\"vertical\"><input type=\"checkbox\" name=\"{1}\" id=\"{1}\" {3} {4} data-entityId=\"{5}\"></input><label for=\"{1}\">{2}</label></fieldset></div>"
							.F(
								"fs" + idStr,//0
								str,//1
								element.Label//2
								, element.Value == "1" ? "checked=\"checked\"" : string.Empty //3
								, SetDisplayOptions(ref element)
								, element.EntityId
							);
					return checkboxHtml;//<<<<<<<<<<<<<<<<<


					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.DateText:
				{
					formElement = new TagBuilder("input");
					formElement.SetType("date");
					formElement.SetNameIdValue(ref element);
					formElement.AddClearBtn();
					break;
				}

				case MobileInputType.TimeText:
				{
					formElement = new TagBuilder("input");
					formElement.SetType("time");
					formElement.SetNameIdValue(ref element);
					formElement.AddClearBtn();
					break;
				}

				case MobileInputType.DateTimeText:
				{
					formElement = new TagBuilder("input");
					formElement.SetType("datetime-local");
					formElement.SetNameIdValue(ref element);
					formElement.AddClearBtn();
					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.TextArea:
				{
					formElement = new TagBuilder("textarea");
					formElement.SetColsRows(40, 8);
					formElement.SetNameId(ref element);
					formElement.SetInnerText(element.Value);
					formElement.AddClearBtn();
					break;
				}

					//----------------------------------------------------------------
				case MobileInputType.YesNo:
				{
					formElement = new TagBuilder("select");
					formElement.SetDataRole("slider");
					formElement.SetNameId(ref element);
					formElement.InnerHtml = CreateYesNoOptions(element.Value == "1");
					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.DropDown:
				{
					formElement = new TagBuilder("select");

					//var innerHtml = new StringBuilder();
					//foreach (var childElem in element.Childs)
					//{
					//    innerHtml.Append(Create(childElem));
					//}
					formElement.SetNameId(ref element);
					formElement.InnerHtml = CreateOptionTagsFromChilds(ref element);
					break;
				}


					//case MobileInputType.DropDownOption:
					//    {
					//        var tag = CreateOptionTag(ref element);
					//        return tag.InnerHtml;
					//        break;
					//    }

					//----------------------------------------------------------------
				case MobileInputType.CollapsibleSet:
				{
					// data-role='collapsible-set' data-theme='a' data-content-theme='a' data-collapsed-icon='carat-d' data-expanded-icon='carat-u' 
					formElement = new TagBuilder("div");
					formElement.SetDataRole("collapsible-set");
					//formElement.SetDataTheme("a");
					formElement.MergeAttribute("data-collapsed-icon", "carat-d");
					formElement.MergeAttribute("data-expanded-icon", "carat-u");

					var innerHtml = new StringBuilder();

					foreach (var childElem in element.Childs)
					{
						innerHtml.Append(Create(childElem));
					}
					formElement.InnerHtml = innerHtml.ToString();
					break;
				}
				case MobileInputType.Collapsible:
				{
					// data-role='collapsible-set' data-theme='a' data-content-theme='a' data-collapsed-icon='carat-d' data-expanded-icon='carat-u' 
					formElement = new TagBuilder("div");
					formElement.SetDataRole("collapsible");
					var innerHtml = new StringBuilder();

					innerHtml.AppendFormat("<h4>{0}</h4>", element.Label);//gonna be Title by JQM

					foreach (var childElem in element.Childs)
					{
						innerHtml.Append(Create(childElem));
					}
					formElement.InnerHtml = innerHtml.ToString();
					return formElement.ToString(); //<<<<<<<<<<<<<<<<<<<<<<<
					break;
				}
					//----------------------------------------------------------------
				case MobileInputType.UnSortedList:
					//<strong >ultest:</strong>
					//<ul id="ultest" data-role="listview" data-inset="true">
					//	<li>Acura</li>
					//	<li>Acura</li>
					//</ul>
					formElement = new TagBuilder("ul");
					formElement.SetDataRole("listview");
					formElement.MergeAttribute("data-inset", "true");

					formElement.InnerHtml = CreateLiTagsFromChilds(ref element);

					string preUl = string.Empty;
					if (element.Label.IsNotNullOrEmpty())
					{
						preUl = CreateLabel(element.Label);
					}
					return preUl + formElement.ToString(); //<<<<<<<<<<<<<<<<<<<<<<<
					break;
					//----------------------------------------------------------------
				case MobileInputType.SortedList:
					formElement = new TagBuilder("ol");
					formElement.SetDataRole("listview");
					formElement.MergeAttribute("data-inset", "true");

					formElement.InnerHtml = CreateLiTagsFromChilds(ref element);

					string preOl = string.Empty;
					if (element.Label.IsNotNullOrEmpty())
					{
						preOl = CreateLabel(element.Label);
					}
					return preOl + formElement.ToString(); //<<<<<<<<<<<<<<<<<<<<<<<
					break;

				default:
					formElement = new TagBuilder(string.Empty);
					break;

			}

			formElement.SetDisplayOptions(ref element);

			if (element.EntityId != 0)
				formElement.MergeAttribute("data-entityId", element.EntityId.ToString(Usa));

			var fieldContainer = new TagBuilder("div");
			fieldContainer.AddCssClass("ui-field-contain");

			if (element.Label.IsNotNullOrEmpty())
			{
				var ll = new TagBuilder("label");
				ll.MergeAttribute("for", formElement.Attributes["name"]);
				ll.SetInnerText(element.Label + ":");
				fieldContainer.InnerHtml += ll;
			}

			fieldContainer.InnerHtml += formElement;
			return fieldContainer.ToString();
		}
		
	}
}