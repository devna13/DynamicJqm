using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class CheckboxTests
	{

		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}


		[TestMethod]
		public void create_checkbox()
		{
			//act
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);

			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);

			//Assert
			TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

			TestHelper.check_element_type_is(str, "checkbox");

			TestHelper.check_element_id_and_name_and_dataentityId(str, "CheckBox", uiElementViewModel.EntityId);

			TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);

			//more asserts
			Assert.IsTrue(str.Contains("<input "));
			Assert.IsTrue(str.Contains("<fieldset "));
			Assert.IsTrue(str.Contains("data-role=\"controlgroup\" data-type=\"vertical\">"));

		}
		[TestMethod]
		public void create_checked_checkbox()
		{
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);
			uiElementViewModel.Value = "1";
			uiElementViewModel.Label = "haha";
			//act
			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);
			TestHelper.check_open_close_tags(str);

			Assert.IsTrue(str.Contains(" checked=\"checked\" "));
		}

		[TestMethod]
		public void create_unchecked_checkbox()
		{
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);
			uiElementViewModel.Value = "0";
			uiElementViewModel.Label = "haha";
			//act
			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);
			TestHelper.check_open_close_tags(str);

			Assert.IsFalse(str.Contains("checked=\"checked\""));
		}
		
        [TestMethod]
        public void create_checkbox_required()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);
            uiElementViewModel.IsRequired = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

		/// <summary>
		/// This doesnt make any sence for checkbox
		/// </summary>
        [TestMethod]
		[Ignore]
		public void create_checkbox_readonly()
        {

            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);
            uiElementViewModel.IsReadOnly = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_checkbox_disabled()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.CheckBox);
            uiElementViewModel.IsDisabled = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

	}
}