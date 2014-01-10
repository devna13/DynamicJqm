using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
    [TestClass]
    public class DateTests
    {
        
		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}
        [TestMethod]
        public void create_DateText()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.DateText);
	        uiElementViewModel.EntityId = 100;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

            TestHelper.check_element_type_is(str, "date");

            TestHelper.check_element_id_and_name_and_dataentityId (str, "DateText", uiElementViewModel.EntityId);

            TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);


            //more asserts
            Assert.IsTrue(str.Contains("<input "));
            Assert.IsTrue(str.Contains(" data-clear-btn=\"true\""));
        }

        [TestMethod]
        public void create_DateText_required()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.DateText);
            uiElementViewModel.IsRequired = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_DateText_readonly()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.DateText);
            uiElementViewModel.IsReadOnly = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_DateText_disabled()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.DateText);
            uiElementViewModel.IsDisabled = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_DateText_with_value_format()
        {
			
			var format = Global.DateFormat;
	        var utcNow = DateTime.UtcNow;
	        var culture = Global.Usa;

            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.DateText);
			uiElementViewModel.Value = utcNow.ToString(format);
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_value_is(str, uiElementViewModel.Value);


			var parsed = DateTime.ParseExact(uiElementViewModel.Value, format, culture);
			Assert.IsTrue(parsed.ToString(format) == utcNow.ToString(format));
        }


    }
}