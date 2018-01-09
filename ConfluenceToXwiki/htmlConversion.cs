using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

//http://www.xwiki.org/xwiki/bin/view/Documentation/UserGuide/Features/XWikiSyntax/

namespace ConfluenceToXwiki
{
    class htmlConversion
    {
        public static string tags(string xwikiformat)
        {
            string replaced = xwikiformat;
            List<object> indexed = new List<object>();

            //Perform HTML cleanup of unused tags for conversion
            replaced = Regex.Replace(replaced, "<p>", "");
            replaced = Regex.Replace(replaced, "&nbsp;", "");
            replaced = Regex.Replace(replaced, "<span>", "");
            replaced = Regex.Replace(replaced, "</span>", "");
            replaced = Regex.Replace(replaced, "</div>", "");
            replaced = Regex.Replace(replaced, "<ul>", "");
            replaced = Regex.Replace(replaced, "</ul>", "");
            replaced = Regex.Replace(replaced, "<fieldset>", "");
            replaced = Regex.Replace(replaced, "</fieldset>", "");
            replaced = Regex.Replace(replaced, "</th>", "");
            replaced = Regex.Replace(replaced, "<tr>", "");
            replaced = Regex.Replace(replaced, "<td>", "");
            replaced = Regex.Replace(replaced, "</td>", "");
            replaced = Regex.Replace(replaced, "<thead>", "");
            replaced = Regex.Replace(replaced, "</thead>", "");
            replaced = Regex.Replace(replaced, "<tbody>", "");
            replaced = Regex.Replace(replaced, "</tbody>", "");
            replaced = Regex.Replace(replaced, "</table>", "");
            replaced = Regex.Replace(replaced, "</form>", "");
            replaced = Regex.Replace(replaced, "<ol>", "");
            replaced = Regex.Replace(replaced, "</ol>", "");
            replaced = Regex.Replace(replaced, "<div>", "");
            replaced = Regex.Replace(replaced, "</p>", "" + System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "&lt;", "<");
            replaced = Regex.Replace(replaced, "&gt;", ">");
            replaced = Regex.Replace(replaced, "<em>", "//");
            replaced = Regex.Replace(replaced, "</em>", "//");
            replaced = Regex.Replace(replaced, @"â€“", "-");
            replaced = Regex.Replace(replaced, @"â€™", "'"); 
            replaced = Regex.Replace(replaced, "<tr class=\"sortableHeader\">", System.Environment.NewLine + "(% class=\"sortHeader\" %)");

            foreach (Match match in Regex.Matches(replaced, "<span class=\"|<span style=\"|<div class=\"|<div id=\"|<h1 id=\"|<h2 id=\"|<h3 id=\"|<h4 id=\"" +
                "|<a class=\"|<br class=\"|<a href=\"|<input name=\"|<fieldset class=\"|<input name=\"|<ul class=\"|<u style=\"|<a style=\"|<strong style=\"" +
                "|<img|<p style=\"|<form name=\"|<td class=\"|<th class=\"|<table class=\"|<input name=\"|<input class=\"|\">"))
            {
                indexed.Add(match.Value);
                indexed.Add(match.Index);
            };
            indexed.Reverse();
            //reverse order otherwise the file shrinks in size and the tag positions are misrepresented in the index

            for (int x = 0; x < indexed.Count; x += 4)
            {
                if ((string)indexed[x + 3] == "<a href=\"")
                {
                    //end tags are read first, skip ahead to beginning tags (+3) 
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    //[x] is the end index, [x +2] is the beginning index
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], "[[");
                }
                else if ((string)indexed[x + 3] == "<img")
                {
                    int srcIndex = replaced.IndexOf("src=", (int)indexed[x + 2]);
                    int endIndex = replaced.IndexOf("?", srcIndex);
                    string srcName = replaced.Substring(srcIndex, endIndex - srcIndex);
                    int indexStart = nthIndex(srcName, "/", 4) + 1; //add one to result to get start of file name without "/"
                    string fileName = srcName.Substring(indexStart, srcName.Length - indexStart);

                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], "image:" + fileName);
                }
                else if ((string)indexed[x + 3] == "<p style=\"")
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], System.Environment.NewLine + "");
                }
                else if ((string)indexed[x + 3] == "<td class=\"")
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], "|");
                }
                else if ((string)indexed[x + 3] == "<th class=\"")
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], "|=");
                }
                else if ((string)indexed[x + 3] == "<a class=\"")
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    string temp = "</a>";
                    int index = replaced.IndexOf(temp, (int)indexed[x]);
                    replaced = replaced.Remove(index, temp.Length);
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                }
                else if (((string)indexed[x + 3] == "<h4 id=\""))
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], System.Environment.NewLine + "====");
                }
                else if (((string)indexed[x + 3] == "<h3 id=\""))
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], System.Environment.NewLine + "===");
                }
                else if (((string)indexed[x + 3] == "<h2 id=\""))
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], System.Environment.NewLine + "==");
                }
                else if (((string)indexed[x + 3] == "<h1 id=\""))
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], System.Environment.NewLine + "=");
                }
                else if (((string)indexed[x + 3] == "<strong style=\""))
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                    replaced = replaced.Insert((int)indexed[x + 2], " **");
                }
                else
                {
                    int length = ((int)indexed[x] - (int)indexed[x + 2]) + 2;
                    //+2 is to account for the ending tag index, we want to delete those as well.
                    replaced = replaced.Remove((int)indexed[x + 2], length);
                }
            }

            //MessageBox.Show(Convert.ToString(replaced.Length));

            //Perform HTML cleanup of unused tags for conversion
            replaced = Regex.Replace(replaced, "</tr>", System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "</a>", "]]");
            replaced = Regex.Replace(replaced, "<u>", "__");
            replaced = Regex.Replace(replaced, "</u>", "__");
            replaced = Regex.Replace(replaced, "<li>", " * ");
            replaced = Regex.Replace(replaced, "<strong>", System.Environment.NewLine + " **");
            replaced = Regex.Replace(replaced, "</strong>", "** ");
            replaced = Regex.Replace(replaced, "</h1>", "= " + System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "</h2>", "== " + System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "</h3>", "=== " + System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "</h4>", "==== " + System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "<br>", System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "</li>", System.Environment.NewLine);
            replaced = Regex.Replace(replaced, "Collapse all", ""); //collapsable menu leftovers removal
            replaced = Regex.Replace(replaced, "Expand all", "");
            replaced = replaced.Insert(0, "="); //conversion is shaving an H1 starting tag for the specified use case

            return replaced;
        }

        //Searches for an n-th instance of searchString inside the inputString and returns the index of said instance inside the inputString
        static int nthIndex(string inputString, string searchString, int instanceNum)
        {
            int returnedIndex = 0;
            for (int x = 0; x < instanceNum; x += 1)
            {
                returnedIndex += 1; //increment by one so that on the next loop it doesn't catch the same instance of the character
                returnedIndex = inputString.IndexOf(searchString, returnedIndex);
            }
            return returnedIndex;
        }
    }    
}