using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
    [TestClass]
    public class TextBoxTests
    {
        
		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}

        #region Textbox

        [TestMethod]
        public void create_textbox()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextBox);
	        uiElementViewModel.EntityId = 100;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

            TestHelper.check_element_type_is(str, "text");

            TestHelper.check_element_id_and_name_and_dataentityId (str, "TextBox", uiElementViewModel.EntityId);

            TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);


            //more asserts
            Assert.IsTrue(str.Contains("<input "));
            Assert.IsTrue(str.Contains(" data-clear-btn=\"true\""));
        }

        [TestMethod]
        public void create_textbox_required()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextBox);
            uiElementViewModel.IsRequired = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_textbox_readonly()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextBox);
            uiElementViewModel.IsReadOnly = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_textbox_disabled()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextBox);
            uiElementViewModel.IsDisabled = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_textbox_with_value()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextBox);
            uiElementViewModel.Value = "text value goes here";
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_value_is(str, uiElementViewModel.Value);
        }

        #endregion

    }
}