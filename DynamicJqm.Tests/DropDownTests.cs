using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class DropDownTests
	{

		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}


		[TestMethod]
		public void create_collapsible_set()
		{
			//act
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.DropDown);
			uiElementViewModel.EntityId = 100;

			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);

			//Assert
			TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

			TestHelper.check_element_id_and_name_and_dataentityId(str, "DropDown", uiElementViewModel.EntityId);

			TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);
			

			Assert.IsTrue(str.Contains("<select "));
		}
	

	}
}