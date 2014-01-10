using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class CollapsibleSetTests
	{

		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}


		[TestMethod]
		public void create_collapsibleset()
		{
			//act
			//arrange 
			var uiElementViewModel = new UiElementViewModel(MobileInputType.CollapsibleSet);

			var str = _sut.Create(uiElementViewModel);
			Console.WriteLine(str);

			TestHelper.check_open_close_tags(str);

			//asserts
			Assert.IsFalse(str.Contains("data-entityId"));

		}
	

	}
}