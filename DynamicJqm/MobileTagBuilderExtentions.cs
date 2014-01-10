using System.Web.Mvc;

namespace DynamicJqm
{
	public static class MobileTagBuilderExtentions
	{
		public static void AddClearBtn(this TagBuilder tb)
		{
			tb.MergeAttribute("data-clear-btn", "true");
		}

		public static void SetDataRole(this TagBuilder tb, string dataRole)
		{
			tb.MergeAttribute("data-role", dataRole);
		}

		public static void SetDataTheme(this TagBuilder tb, string theme)
		{
			tb.MergeAttribute("data-theme", theme);
		}

		public static void SetType(this TagBuilder tb, string type)
		{
			tb.MergeAttribute("type", type);
		}

		public static void SetChecked(this TagBuilder tb, bool isChecked)
		{
			if (isChecked)
			{
				tb.MergeAttribute("checked", "checked");
			}
		}


		public static void SetValue(this TagBuilder tb, ref UiElementViewModel elem)
		{
			if (elem.Value.IsNotNullOrEmpty())
				tb.MergeAttribute("value", elem.Value);
		}
		public static void SetNameId(this TagBuilder tb, ref UiElementViewModel elem)
		{
			var str = "{0}_{1}".F(elem.MobileInputType.Str(), elem.EntityId);
			tb.MergeAttribute("name", str);
			tb.MergeAttribute("id", str);
		}
		public static void SetNameIdValue(this TagBuilder tb, ref UiElementViewModel elem)
		{
			tb.SetNameId(ref elem);
			tb.SetValue(ref elem);
		}


		public static void SetCols(this TagBuilder tb, int cols)
		{
			tb.MergeAttribute("cols", cols.ToString(Global.Usa));
		}
		public static void SetRows(this TagBuilder tb, int rows)
		{
			tb.MergeAttribute("rows", rows.ToString(Global.Usa));
		}

		public static void SetColsRows(this TagBuilder tb, int cols, int rows)
		{
			tb.SetCols(cols);
			tb.SetRows(rows);
		}



		public static void SetDisplayOptions(this TagBuilder tb, ref UiElementViewModel elem)
		{
			if (elem.IsReadOnly)
			{
				tb.MergeAttribute("readonly", "readonly");
				//tb.AddCssClass("readonly");//<<<<<<<<<<<<<<<<<<<<<<
			}

			if (elem.IsRequired)
			{

				tb.MergeAttribute("required", "required");
				//tb.AddCssClass("required");//<<<<<<<<<<<<<<<
			}

			if (elem.IsSelected)
			{
				tb.MergeAttribute("selected", "selected");//<<<<<<<<<<<<<<<
			}

			if (elem.IsDisabled)
			{
				tb.MergeAttribute("disabled", "disabled");
				//tb.AddCssClass("disabled");
			}
		}


	}
}