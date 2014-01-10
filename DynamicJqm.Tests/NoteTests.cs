using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
    [TestClass]
    public class NoteTests
    {
        
		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}


        [TestMethod]
        public void create_Note()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.Note);
	        uiElementViewModel.EntityId = 100;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_wrapped_inside_a_div_with_uifieldcontain_class(str);

            TestHelper.check_element_display_options(str, ref uiElementViewModel);

			TestHelper.check_open_close_tags(str);


            //more asserts
            Assert.IsTrue(str.Contains("<strong "));
        }

     
        [TestMethod]
        public void create_Note_disabled()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.Note);
            uiElementViewModel.IsDisabled = true;
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert
            TestHelper.check_element_display_options(str, ref uiElementViewModel);
        }

        [TestMethod]
        public void create_Note_with_value()
        {
            //arrange 
            var uiElementViewModel = new UiElementViewModel(MobileInputType.Note);
            uiElementViewModel.Value = "text value goes here";
            //act
            var str = _sut.Create(uiElementViewModel);
            Console.WriteLine(str);

            //Assert

			//Assert
			Assert.IsTrue(str.Contains(">{0}<".F(uiElementViewModel.Value)));
        }

    }
}