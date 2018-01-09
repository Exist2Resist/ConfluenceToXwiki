using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;

namespace ConfluenceToXwiki
{
    class conversion
    {
        const string startPattern = "<a class=\"current ancestor-link\" href=";
        const string endPattern = "<div class=\"clear-fix\"></div>";
        const string imgStartPttrn = "data-image-src=";

        public static void sortData(string pageContent, string urlAddress)
        {
            //Sorts through the capured body data
            string repString = Regex.Replace(urlAddress, @"^https?|kb.printaudit.com?|display?|[:/+.]", "");
            string fileName = repString + "_converted_" + ".txt"; //added _converted_ for testing
            string xwikiFormat = stripData(pageContent); //raw captured body data
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\ConfluenceToXwiki\\" + repString + "\\";
            string fileAndPath = folderPath + fileName;
            string converted = htmlConversion.tags(convertToUTF8(xwikiFormat)); //strips HTML data and converts to Xwiki

            imageSave(folderPath, xwikiFormat, urlAddress);
            storeData(folderPath, fileAndPath, converted); //saves converted content to txt file           
            storeData(folderPath, folderPath + repString + ".txt", xwikiFormat); //saves raw captured HTML to txt file            
            //storeData(folderPath, folderPath + repString + "_UTF8.txt", convertToUTF8(xwikiFormat));
        }

        public static void storeData(string directory, string fileAndPath, string pageContent)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var binFormatter = new BinaryFormatter();                      

            if (!File.Exists(fileAndPath))
            {
                FileStream fs = new FileStream(fileAndPath, FileMode.Create);
                binFormatter.Serialize(fs, pageContent);
                fs.Close();
            }
            else
            {
                MessageBox.Show("Text body content file already exists!\nDelete folder contents if you want to create a new copy.");
            }
        }
                
        public static string stripData(string pageContent)
        {
            int indexStart = pageContent.IndexOf(startPattern);
            int indexEnd = pageContent.IndexOf(endPattern);
            int indexLength = indexEnd - indexStart;           
            string stripString = pageContent.Substring(indexStart, indexLength);            
            //MessageBox.Show(stripString);
            return stripString;
        }

        public static void imageSave(string folderPath, string xwikiFormat, string urlAddress)
        {
            int indexEnd = 0;
            string imgRegEx = @"[^\s]+(\.(jpg|gif|png))";
            //var match = Regex.Match(xwikiFormat, imgRegEx);
            using (WebClient webC = new WebClient())
            {
                string storedFileName = "";
                foreach (Match match in Regex.Matches(xwikiFormat, imgRegEx))
                {                
                    indexEnd = match.Index;
                    string imgPathName = xwikiFormat.Substring(indexEnd + 5, match.Length - 5);
                
                    string urlStripAddress = urlAddress.Substring(0, urlAddress.IndexOf(@"/display/"));

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    int indexFileNameStart = imgPathName.LastIndexOf(@"/");
                    int indexLength = imgPathName.Length - indexFileNameStart;

                    string imgFileName = imgPathName.Substring(indexFileNameStart + 1, indexLength - 1);
                    try
                    {
                        if (storedFileName != imgFileName)
                        {
                            storedFileName = imgFileName;
                            if (!File.Exists(folderPath + imgFileName))
                            {
                                webC.DownloadFile(urlStripAddress + imgPathName, folderPath + imgFileName);                               
                            }
                            else
                            {
                                MessageBox.Show("Image file already exists in the Folder!\nDelete folder contents if you want to create a new copy.");
                            }
                        }
                        else if (storedFileName == imgFileName)
                        {
                            //RegEx Match only needs to match once. 
                            //Remove redundancy, else if not necessary                            
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Unable to fetch and save image file!\nMake sure that the image(s) on the page\nare not being fetched from Google.");
                    }
                }
            }
        }
        //The below conversion is necessary otherwise non standard characters such as â€™ which is a (') and â€“ which is a (-) appear in converted text
        public static string convertToUTF8(string xwikiFormat)
        {
            Encoding isoEnconding = Encoding.GetEncoding("iso-8859-1");
            Byte[] source = isoEnconding.GetBytes(xwikiFormat);
            return Encoding.UTF8.GetString(Encoding.Convert(isoEnconding, Encoding.UTF8, source));           
        }
    }
}