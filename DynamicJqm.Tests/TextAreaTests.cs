using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
    [TestClass]
    public class TextAreaTests
    {
        
		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}

        [TestMethod]
        public void create_TextArea()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextArea);
	        uiElementViewModel.EntityId = 100;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

          //  TestHelper.check_element_type_is(str, "text");

            TestHelper.check_element_id_and_name_and_dataentityId (str, "TextArea", uiElementViewModel.EntityId);

            TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);


            //more asserts
            Assert.IsTrue(str.Contains("<textarea "));
            Assert.IsTrue(str.Contains(" data-clear-btn=\"true\""));
        }

        [TestMethod]
        public void create_TextArea_required()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextArea);
            uiElementViewModel.IsRequired = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_TextArea_readonly()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextArea);
            uiElementViewModel.IsReadOnly = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_TextArea_disabled()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextArea);
            uiElementViewModel.IsDisabled = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_TextArea_with_value()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.TextArea);
            uiElementViewModel.Value = "text value goes here";
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            Assert.IsTrue( str.Contains(">{0}<".F(uiElementViewModel.Value)) );
        }

    }
}