using AngleSharp.Html.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.LinkLabel;

namespace WinFormsApp1.Core.GitHub
{
    internal class GitParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var list=new List<string>();

            var items = document.QuerySelectorAll("h2.tm-title").Select(item => item.TextContent.Trim());

            list.AddRange(items);
            return list.ToArray();
        }
    }
}
