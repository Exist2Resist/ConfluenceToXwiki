Confluence to Xwiki covnersion tool.
This tool was written to convert a Knowledge base in Confluence to an Xwiki format. 
The main KB page can not be converted and the URL is hardcoded into the Form to take the user to the main page of the KB
when the executable is launched.

Essentially all this does is indexes the HTML tags, strips them, and replaces them with wiki formatting. 
It then grabs all the images on the page in the body and it stores both the reformatted text and images in the Users docmuments folder.

Two copies of the file are saved in the users documents folder, a raw TXT file and a Converted TXT file. 
The converted text file contains all the pertaining Xwiki Formatting.
The raw text file has all the original HTML tags.

The Converted text file has a lot of empty space, this is due to the automatic removal of the tags and the conversion process. 
There will be some user intervention necessary. 
Also at the beginning of the converted file there is some jibberish due to the filestream method and how it works in .NET.

For references the Xwiki Syntaxes can be found here.