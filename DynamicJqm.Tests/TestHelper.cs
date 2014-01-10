using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicJqm.Tests
{
	public static class TestHelper
	{
		#region Helpers

		public static void check_element_wrapped_inside_a_div_with_uifieldcontain_class(string htmlStr)
		{
			//Console.WriteLine(htmlStr);
			Assert.IsTrue(htmlStr.StartsWith("<div class=\"ui-field-contain\">"));
			Assert.IsTrue(htmlStr.EndsWith("</div>"));
		}

		public static void check_element_type_is(string htmlStr, string type)
		{
			Assert.IsTrue(htmlStr.Contains(" type=\"" + type + "\""));
		}

		public static void check_element_value_is(string htmlStr, string value)
		{
			Assert.IsTrue(htmlStr.Contains(" value=\"" + value + "\""));
		}

		public static void check_element_id_and_name_and_dataentityId(string htmlStr, string prefix, int entityId)
		{
			check_element_id_and_name(htmlStr, prefix, entityId);
			check_element_dataentityId(htmlStr, entityId);
		}

		public static void check_element_id_and_name(string htmlStr, string prefix, int entityId)
		{
			var idStr = entityId.ToString(Global.Usa);
			Assert.IsTrue(htmlStr.Contains(" id=\"{0}_{1}\"".F(prefix, idStr)));
			Assert.IsTrue(htmlStr.Contains(" name=\"{0}_{1}\"".F(prefix, idStr)));
		}

		public static void check_element_dataentityId(string htmlStr, int entityId)
		{
			var idStr = entityId.ToString(Global.Usa);
			Assert.IsTrue(htmlStr.Contains(" data-entityId=\"{0}\"".F(idStr)));
		}

		public static void check_open_close_tags(string htmlStr)
		{
			var open = htmlStr.CountStringOccurrences("<");
			var close = htmlStr.CountStringOccurrences(">");
			var end = htmlStr.CountStringOccurrences("</");

			Console.WriteLine("Open: " + open);
			Console.WriteLine("Close: " + close);
			Console.WriteLine("End: " + end);
			Assert.AreEqual(open, close);


			string error = TestHelper.CheckHtml(htmlStr);
			Console.WriteLine("Errors: " + error);
			Assert.AreEqual(error, "");
		}


		public static void check_element_display_options(string htmlStr, ref UiElementViewModel elem)
		{
			if (elem.IsReadOnly)
			{
				Assert.IsTrue(htmlStr.Contains(" readonly=\"{0}\"".F("readonly")));
				//Assert.IsTrue(htmlStr.Contains(" class=\"{0}\"".F("readonly")));
			}

			if (elem.IsRequired)
			{
				//Assert.IsTrue(htmlStr.Contains(" class=\"{0}\"".F("required"))); //<<<<<<<<<<<<<<<<<
				Assert.IsTrue(htmlStr.Contains(" required=\"{0}\"".F("required")));
			}

			if (elem.IsSelected)
			{
				Assert.IsTrue(htmlStr.Contains(" selected=\"{0}\"".F("selected")));

			}

			if (elem.IsDisabled)
			{
				Assert.IsTrue(htmlStr.Contains(" disabled=\"{0}\"".F("disabled")));
			}
		}



		#endregion



		/// <summary>
		/// Whether the HTML is likely valid. Error parameter will be empty
		/// if no errors were found.
		/// </summary>
		static public string CheckHtml(string html)
		{
			//
			// Store our tags in a stack
			//
			Stack<string> tags = new Stack<string>();

			//
			// Initialize out parameter to empty
			//
			string error = string.Empty;

			//
			// Count of parenthesis
			//
			int parenthesisR = 0;
			int parenthesisL = 0;

			//
			// Traverse entire HTML
			//
			for (int i = 0; i < html.Length; i++)
			{
				char c = html[i];
				if (c == '<')
				{
					bool isClose;
					bool isSolo;

					//
					// Look ahead at this tag
					//
					string tag = LookAhead(html, i, out isClose, out isSolo);

					//
					// Make sure tag is lowercase
					//
					if (tag.ToLower() != tag)
					{
						error = "upper: " + tag;
						return error;
					}

					//
					// Make sure solo tags are parsed as solo tags
					//
					if (_soloTags.ContainsKey(tag))
					{
						if (!isSolo)
						{
							error = "!solo: " + tag;
							return error;
						}
					}
					else
					{
						//
						// We are on a regular end or start tag
						//
						if (isClose)
						{
							//
							// We can't close a tag that isn't on the stack
							//
							if (tags.Count == 0)
							{
								error = "closing: " + tag;
								return error;
							}

							//
							// Tag on stack must be equal to this closing tag
							//
							if (tags.Peek() == tag)
							{
								//
								// Remove the start tag from the stack
								//
								tags.Pop();
							}
							else
							{
								//
								// Mismatched closing tag
								//
								error = "!match: " + tag;
								return error;
							}
						}
						else
						{
							//
							// Add tag to stack
							//
							tags.Push(tag);
						}
					}
					i += tag.Length;
				}
				else if (c == '&')
				{
					//
					// & must never be followed by space or other &
					//
					if ((i + 1) < html.Length)
					{
						char next = html[i + 1];

						if (char.IsWhiteSpace(next) ||
							next == '&')
						{
							error = "ampersand";
							return error;
						}
					}
				}
				else if (c == '\t')
				{
					error = "tab";
					return error;
				}
				else if (c == '(')
				{
					parenthesisL++;
				}
				else if (c == ')')
				{
					parenthesisR++;
				}
			}

			//
			// If we have tags in the stack, write them to error
			//
			foreach (string tagName in tags)
			{
				error += "extra:" + tagName + " ";
			}

			//
			// Require even number of parenthesis
			//
			if (parenthesisL != parenthesisR)
			{
				error = "!even ";
			}
			return error;
		}

		/// <summary>
		/// Called at the start of an html tag. We look forward and record information
		/// about our tag. Handles start tags, close tags, and solo tags. 'Collects'
		/// an entire tag.
		/// </summary>
		/// <returns>Tag name.</returns>
		static private string LookAhead(string html, int start, out bool isClose,
			out bool isSolo)
		{
			isClose = false;
			isSolo = false;

			StringBuilder tagName = new StringBuilder();

			//
			// Stores the position of the final slash
			//
			int slashPos = -1;

			//
			// Whether we have encountered a space
			//
			bool space = false;

			//
			// Whether we are in a quote
			//
			bool quote = false;

			//
			// Begin scanning the tag
			//
			int i;
			for (i = 0; ; i++)
			{
				//
				// Get the position in main html
				//
				int pos = start + i;

				//
				// Don't go outside the html
				//
				if (pos >= html.Length)
				{
					return "x";
				}

				//
				// The character we are looking at
				//
				char c = html[pos];

				//
				// See if a space has been encountered
				//
				if (char.IsWhiteSpace(c))
				{
					space = true;
				}

				//
				// Add to our tag name if none of these are present
				//
				if (space == false &&
					c != '<' &&
					c != '>' &&
					c != '/')
				{
					tagName.Append(c);
				}

				//
				// Record position of slash if not inside a quoted area
				//
				if (c == '/' &&
					quote == false)
				{
					slashPos = i;
				}

				//
				// End at the > bracket
				//
				if (c == '>')
				{
					break;
				}

				//
				// Record whether we are in a quoted area
				//
				if (c == '\"')
				{
					quote = !quote;
				}
			}

			//
			// Determine if this is a solo or closing tag
			//
			if (slashPos != -1)
			{
				//
				// If slash is at the end so this is solo
				//
				if (slashPos + 1 == i)
				{
					isSolo = true;
				}
				else
				{
					isClose = true;
				}
			}

			//
			// Return the name of the tag collected
			//
			string name = tagName.ToString();
			if (name.Length == 0)
			{
				return "empty";
			}
			else
			{
				return name;
			}
		}

		/// <summary>
		/// Tags that must be closed in the start
		/// </summary>
		static Dictionary<string, bool> _soloTags = new Dictionary<string, bool>()
		{
			{"img", true},
			{"br", true}
		};

	}
}