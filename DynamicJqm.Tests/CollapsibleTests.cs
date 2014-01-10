using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class CollapsibleTests
	{

		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}


		[TestMethod]
		public void create_collapsible()
		{
			//act
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.Collapsible);

			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);

			TestHelper.check_open_close_tags(str);

			//asserts
			Assert.IsFalse(str.Contains("data-entityId"));
			Assert.IsTrue(str.Contains("<h4>{0}</h4>".F(uiElementViewModel.Label)));
		}
		[TestMethod]
		public void create_collapsible_with_header()
		{
			//act
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.Collapsible);
			uiElementViewModel.Label = "Title goes here";

			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);

			TestHelper.check_open_close_tags(str);

			//asserts
			Assert.IsFalse(str.Contains("data-entityId"));
			Assert.IsTrue(str.Contains("<h4>{0}</h4>".F(uiElementViewModel.Label)));
		}

	}
}