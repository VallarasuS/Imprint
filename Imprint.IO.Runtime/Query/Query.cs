using System.IO;
using System.Reflection;

namespace Imprint.IO.Runtime
{
	public class QueryBuilder
	{
		public string GetJQuery()
		{
			return GetEmbeddedResourceString("jquery.js");
		}

		public string GetXpath()
		{
			return GetEmbeddedResourceString("xpath.js");
		}

		private string GetEmbeddedResourceString(string file)
		{
			try
			{
				var res = string.Format("{0}.{1}", GetType().Namespace, file);

				var assembly = Assembly.GetExecutingAssembly();
				var textStreamReader = new StreamReader(assembly.GetManifestResourceStream(res));

				return textStreamReader.ReadToEnd();
			}
			catch
			{
				return string.Empty;
			}
		}

		public string EventSubscribe(string functionPointer, Events events)
		{
			var e = string.Empty;

			if(events == Events.MouseUp)
				e = "onmouseup";

			var template = @"
			
			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};


				document.{0} = function(e) {{

				{1}(e);
				
				_removeClass();

			}};";

			return string.Format(template,e,functionPointer);
		}

		public string EventUnSubscribe(Events events)
		{
			var e = string.Empty;

			if (events == Events.MouseUp)
				e = "onmouseup";

			var template = @"$(document).{0} = function(e) {{ }};";

			return string.Format(template, e);
		}

		public string EnableHighlight()
		{
			var script = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

			var __css = ' .___track___ { background-color : yellow; border : thin solid black }';
			var head = document.head || document.getElementsByTagName('head')[0];
			var style = document.createElement('style');
			style.type = 'text/css';
			if (style.styleSheet) {{
				style.styleSheet.cssText = __css;
			}}
			else {{
				style.appendChild(document.createTextNode(__css));
			}}

			head.appendChild(style);

			$(document).mousemove( 
			
			function(e) {{
				
				_removeClass();

				$(e.target).addClass('___track___');

			}} 

			);";

			return script;
		}

		public string HighlightList()
		{
			var script = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

			var __css = ' .___track___ { background-color : yellow; border : thin solid black; }';
			var head = document.head || document.getElementsByTagName('head')[0];
			var style = document.createElement('style');
			style.type = 'text/css';
			if (style.styleSheet) {{
				style.styleSheet.cssText = __css;
			}}
			else {{
				style.appendChild(document.createTextNode(__css));
			}}

			head.appendChild(style);
			
			$(document).mousemove(

				function(e) {{

					if ($(e.target).find('li').length >= 1) {{

						_removeClass();

						_elementTrack = $(e.target).find('li:first')[0];

						if($(_elementTrack).parentsUntil(e.target).length <= 1) {{

							$(e.target).find('li').addClass('___track___');
						}}

					}}
			}}

)";

			return script;
		}

		public string HighlightURL()
		{
			var script = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

			var __css = ' .___track___ { background-color : yellow; border : thin solid black; }';
			var head = document.head || document.getElementsByTagName('head')[0];
			var style = document.createElement('style');
			style.type = 'text/css';
			if (style.styleSheet) {{
				style.styleSheet.cssText = __css;
			}}
			else {{
				style.appendChild(document.createTextNode(__css));
			}}

			head.appendChild(style);
			
			$(document).mousemove(

				function(e) {{

					if ($(e.target).find('a').length >= 1) {{

						_removeClass();

						_elementTrack = $(e.target).find('a:first')[0];

						if($(_elementTrack).parentsUntil(e.target).length <= 1) {{

							$(e.target).find('a').addClass('___track___');
						}}

					}}
			}}

)";

			return script;
		}

		public string HighlightImage()
		{
			var script = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

			var __css = ' .___track___ { background-color : yellow; border : thin solid black; padding: 5px 5px; }';
			var head = document.head || document.getElementsByTagName('head')[0];
			var style = document.createElement('style');
			style.type = 'text/css';
			if (style.styleSheet) {{
				style.styleSheet.cssText = __css;
			}}
			else {{
				style.appendChild(document.createTextNode(__css));
			}}

			head.appendChild(style);
			
			$(document).mousemove(

				function(e) {{

					if ($(e.target).find('img').length >= 1) {{

						_elementTrack = $(e.target).find('img:first')[0];

						if(_elementTrack !== $('.___track___')[0])
							_removeClass();

						if($(_elementTrack).parentsUntil(e.target).length <= 1) {{

							$(e.target).find('img:first').addClass('___track___');
						}}

					}}
					else if($(e.target).siblings().find('img').length >= 1) {{

							_elementTrack = $(e.target).siblings().find('img:first')[0];

							if(_elementTrack !== $('.___track___')[0])
								_removeClass();

							$(e.target).siblings().find('img:first').addClass('___track___');
					}}
			}}

)";

			return script;
		}

		public string DisableHighlight()
		{
			var script = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

				_removeClass();
				$(document).off();

			";

			return script;
		}

		public string IdentifyTagUnderMouse(string functionPointer, string binder, string callback, string type)
		{
			var template = @"

			var _removeClass = function() {{ 

					var x = $('.___track___').removeClass('___track___');

					$('.___track___').each(function(i) {{ 			
						if($(this).className === '') {{ $(element).removeAttr('class'); }}
					}});
			}};

			var {0} = function(e) {{

			var __x = $('.___track___')[0]

			_removeClass();
			
			if(__x.className === '') {{
				$(__x).removeAttr('class');
			}}

			if(__x.tagName === '{3}' || 'TEXT' === '{3}')
			{{
				var ___x = $(__x).closest('[class]')[0];

				if('{3}' === 'A')
					var ___x = $(__x).parent().closest('[class]')[0];

				var tagName = ___x.tagName;

				var className = ___x.className;

				var id = ___x.id;

				var xpath = Xpath.getElementXPath(___x).toString();

				{1}.{2}(tagName, className, id, xpath);

				$(document).off();
			}}

			}};";

			return string.Format(template, functionPointer, binder, callback, type);
		}

		public string ExtractListData(string callerId, string key, Selector selector, string binder, string callback, string header, Element type)
		{
			var template = @"
	
			var xeval = function () {{

			var classCount = '{1}'.split('.').length;

			$('{0}{1}').each(function(ii, ie) {{ 

			if($(ie).attr('class').split(/\s+/).length !== classCount )
				return true;

			$(ie).find('li').each(function(i,e) {{

					var content = e.textContent;

					var image = '';

					var style = e.currentStyle || window.getComputedStyle(e, false),
					image = style.backgroundImage.slice(4, -1);
					// For IE we need to remove quotes to the proper url
					image = style.backgroundImage.slice(4, -1).replace(/'/g, '');

					// override when img tag is found
					if(e.tagName == 'IMG')
					{{
						image = e.src;
					}}
					else if ($(e).find('img').length >= 1) {{
						image = $(e).find('img').attr('src');
					}}
					else {{
						image = $(e).siblings().find('img').attr('src');
					}}

					var url = $(e).find('[href]').attr('href');

					{2}.{3}('{4}', content,$(e).val().toString(), image, url, '{5}', '{6}');

				}});
			}});

			{2}.{3}('{4}', '-EOF-','-EOF-', '-EOF-', '-EOF-', '{5}', '{6}');
}};


			xeval();
";

			if (selector == Selector.Class)
			{
				return string.Format(template, ".", key, binder, callback, callerId, header, type.ToString());
			}

			return string.Empty;
		}

		public string ExtractData(string callerId, string key, Selector selector, string binder, string callback, string header, Element type)
		{
			var template = @"
	
			var xeval = function () {{

			var classCount = '{1}'.split('.').length;

			$('{0}{1}').each(function(i,e) {{ 

			if($(e).attr('class').split(/\s+/).length !== classCount )
				return true;

			var content = e.textContent;

			var image = '';

			var style = e.currentStyle || window.getComputedStyle(e, false),
			image = style.backgroundImage.slice(4, -1);
			// For IE we need to remove quotes to the proper url
			image = style.backgroundImage.slice(4, -1).replace(/'/g, '');

			// override when img tag is found
			if(e.tagName == 'IMG')
			{{
				image = e.src;
			}}
			else if ($(e).find('img').length >= 1) {{
				image = $(e).find('img').attr('src');
			}}
			else {{
				image = $(e).siblings().find('img').attr('src');
			}}


			var url = $(e).find('[href]').attr('href');

			{2}.{3}('{4}', content,$(e).val(), image, url, '{5}', '{6}');

			}}) 

			{2}.{3}('{4}', '-EOF-','-EOF-', '-EOF-', '-EOF-', '{5}', '{6}');
}};


			xeval();
";

			if (selector == Selector.Class)
			{
				return string.Format(template, ".", key, binder, callback, callerId, header, type.ToString());
			}

			return string.Empty;
		}

		// TODO : Create members to build the query using existing frame works

		// ex. GetElementsByTagClass(hook, tag, class) { return "$(tag.class).each(function(i) { hook.FireFount( $(this).text() )}" };

		public string GetScroll()
		{
			var query = @"

				$(document).scrollTop(document.body.scrollHeight);
";

			return query;
		}
	}
}