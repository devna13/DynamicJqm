using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	[TestClass]
	public class MobileTagFactoryTests
	{
		private MobileTagFactory _sut;

		[TestInitialize]
		public void Initialize()
		{
			_sut = new MobileTagFactory();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void create_null_uiElementViewModel()
		{
			_sut.Create(null);
		}


		[TestMethod]
		[Ignore]
		public void create_undefined_mobileInputType()
		{
			var str = _sut.Create(null//new UiElementViewModel() cant create uiviewmodel with unspecified type
				);

			Console.WriteLine(str);
		}
	}
}
