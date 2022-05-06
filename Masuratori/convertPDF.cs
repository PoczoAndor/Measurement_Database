using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using Dapper;
using System.Configuration;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;

namespace Masuratori
{
    class convertPDF
    {
        private static string pathPdf(string a)//path of the pdf measurements
        {
            return a;
            
        }
        public static void watcherPathPdfConvert()//convert the pdf documents to text at set time interval
        {
            //string[] readText = File.ReadAllLines(@".\Data_compared.txt");
            string[] files = File.ReadAllLines(@".\Data_compared.txt");
            int numberoOfFiles = files.Length;
            
            int numberOfLoops = 0;


            while (numberoOfFiles != numberOfLoops)//search the folder for all the pdf documents

            {
                
                var info = new FileInfo(files[numberOfLoops]);
                using (var pdf = PdfDocument.Open(files[numberOfLoops]))//open every single pdf document
                {

                    foreach (var page in pdf.GetPages())//convert the pdf document into textfiles
                    {
                        var text = ContentOrderTextExtractor.GetText(page);
                        string fileReper = info.Directory.Name.ToString();
                        string fileDate = info.LastWriteTime.ToString();
                        string fileName = info.Name.ToString();
                        
                        string fileNameCombined = System.IO.Path.Combine(@".\DataConvertedWatch", fileName + ".txt");
                        using (var TextFile = new StreamWriter(fileNameCombined, true))
                        {
                            TextFile.WriteLine("!" + fileReper);//get the reper
                            TextFile.WriteLine("!" + fileDate);//get the date
                            TextFile.WriteLine("!" + fileName);//get the name of the pdf
                            TextFile.WriteLine(text);//write the contents of the pdf
                            TextFile.Close();

                        }
                    }

                }
                string importedText = (@".\Data_Converted.txt");//write in a text what has been converted
                using (var TextFile = new StreamWriter(importedText, true))
                {
                    string imported = files[numberOfLoops];
                    TextFile.WriteLine(imported);

                    TextFile.Close();

                }
                string imported_convert = (@".\Data_Converted.txt");
                using (var TextFile = new StreamWriter(imported_convert, true))
                {
                    string imported = files[numberOfLoops];
                    TextFile.WriteLine(imported);

                    TextFile.Close();

                }
                numberOfLoops++;
            }
        }

        public static void pathPdfConvert(string path )//converting pdf documents to text but without time interval only once
        {
            
            string[] files = Directory.GetFiles(pathPdf(path), "*.pdf", SearchOption.AllDirectories);
            int numberoOfFiles = files.Length;
            int numberOfLoops = 0;
           

            while (numberoOfFiles!=numberOfLoops)
                
            {
                var info = new FileInfo(files[numberOfLoops]);
                using (var pdf = PdfDocument.Open(files[numberOfLoops]))
                {

                    foreach (var page in pdf.GetPages())
                    {
                        var text = ContentOrderTextExtractor.GetText(page);
                        string fileReper = info.Directory.Name.ToString();
                        string fileDate = info.LastWriteTime.ToString();
                        string fileName = info.Name.ToString();

                        string fileNameCombined = System.IO.Path.Combine(@".\DataConverted", fileName + ".txt");
                        using (var TextFile = new StreamWriter(fileNameCombined, true))
                        {
                            TextFile.WriteLine("!" + fileReper);
                            TextFile.WriteLine("!" + fileDate);
                            TextFile.WriteLine("!" + fileName);
                            TextFile.WriteLine(text);
                            TextFile.Close();
                            
                        }
                    }
                    
                }
                string importedText = (@".\Data_Converted.txt");
                using (var TextFile = new StreamWriter(importedText, true))
                {
                    string imported = files[numberOfLoops];
                    TextFile.WriteLine(imported);

                    TextFile.Close();

                }
                numberOfLoops++;
                double progress = (double)numberOfLoops / (double)numberoOfFiles;
                double progressDone = (double)progress * 100;
                Form1.backgroundworkerConvertInstance.ReportProgress(Convert.ToInt32(progressDone));
                
            }

            
        }

    }
}
