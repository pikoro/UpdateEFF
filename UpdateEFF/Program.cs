using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace UpdateEFF
{
    public static class Globals
    {

        public const string TempDir = "C:\\EFF\\";
        public const string TempDatDir = "DAT\\";
        public const string TempEFFDir = "EFF\\";
        public const string FlySmartVersion = "12AUG2018";
        public const bool debug = true;
    }

    class Program
    {


        static void Main(string[] args)
        {
            string effName = args[0];
            UnpackEFF(effName);
            ExtractDat();
            //NormalizeName(effName);
        }

        static string NormalizeName(string effName)
        {
            if (Globals.debug) { Console.WriteLine("Input Name: " + effName); }
            string extension = (".eff");
            effName = effName.TrimEnd(extension.ToCharArray());
            if (Globals.debug) { Console.WriteLine("Fully Trimmed String: " + effName); }
            return effName.ToString();
        }

        static void UnpackEFF(string filename)
        {
            Console.WriteLine("Normalizing Name");

            string Effzipfile = NormalizeName(filename);
            Console.WriteLine("Unpacking EFF " + filename);
            if (Globals.debug) { Console.WriteLine("EFF Name: " + Effzipfile); }
            Effzipfile = Globals.TempDir + Effzipfile + ".eff";
            if (Globals.debug) { Console.WriteLine("File Name: " + Effzipfile); }
            string outpath = Globals.TempDir;
            if (Globals.debug) { Console.WriteLine("OutPath: " + outpath); }

            try { ZipFile.ExtractToDirectory(Effzipfile, outpath); }
            catch { Console.WriteLine("! Contents already extracted"); }
        }

        static void ExtractDat()
        {
            Console.WriteLine("Extracting EFF Contents");
	        string[] EffdatList = Directory.GetFiles(Globals.TempDir, "*.dat", SearchOption.TopDirectoryOnly);
            string Effdatfile = EffdatList[0];
            if (Globals.debug) { Console.WriteLine("DAT Name: " + Effdatfile); }
            if (Globals.debug) { Console.WriteLine("File Name: " + Effdatfile); }
            string outpath = Globals.TempDir + Globals.TempDatDir;
            if (Globals.debug) { Console.WriteLine("OutPath:  " + outpath); }
            try { ZipFile.ExtractToDirectory(Effdatfile, outpath); }
	        catch { Console.WriteLine("! Contents already extracted"); }
        }

        static void ReplaceEffDate()
        {
            Console.WriteLine("Changing EFF Date");
            string xmlFile = Globals.TempDatDir + "eff.xml";
	        if (Globals.debug) { Console.WriteLine("EFF XML: " + xmlFile); }

            XmlDocument doc = new XmlDocument
            {
                PreserveWhitespace = true
            };
            try { doc.Load(xmlFile); }
            catch (System.IO.FileNotFoundException) { Console.WriteLine("! EFF XML file not found"); }

            string DateStamp = doc["EFUSUB.SubFolder.SubFolder.Document"];
	        /*foreach($element in $DateStamp){
		        try {$id = $element.id.ToString()
            } catch {"! Element does not exist"}
		        try {if($debug){"Date Before: " + $element.UpdateDateTime.ToString()}} catch {"! Could not convert null value"}
		        $newDate = Get-Date -Format yyyy-MM-ddThh:mm:ss.fff
		        try{$element.updateDateTime = $newDate.ToString()}
		        catch{"! This node does not have a date"}
		        if($debug){"Updated $id to $newDate"}
	        }
	        $xmlDocument.EFUSUB.M633Header.timestamp = $newDate.ToString()
	        try{$XmlDocument.Save($xmlFile)}
	        catch{"! Unable to Save $xmlfile"}
	        if($debug){"Date set to $newDate"}
            */
        }
    }
}
