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
        private static string pathPdf(string a)
        {
            return a;
            
        }
        public static void watcherPathPdfConvert()
        {
            //string[] readText = File.ReadAllLines(@".\Data_compared.txt");
            string[] files = File.ReadAllLines(@".\Data_compared.txt");
            int numberoOfFiles = files.Length;
            
            int numberOfLoops = 0;


            while (numberoOfFiles != numberOfLoops)

            {
                
                var info = new FileInfo(files[numberOfLoops]);
                using (var pdf = PdfDocument.Open(files[numberOfLoops]))
                {

                    foreach (var page in pdf.GetPages())
                    {
                        // Either extract based on order in the underlying document with newlines and spaces.
                        var text = ContentOrderTextExtractor.GetText(page);

                        // Or based on grouping letters into words.
                        // var otherText = string.Join(" ", page.GetWords());

                        // Or the raw text of the page's content stream.
                        //var text = page.Text;
                        string fileReper = info.Directory.Name.ToString();
                        string fileDate = info.LastWriteTime.ToString();
                        string fileName = info.Name.ToString();
                        
                        string fileNameCombined = System.IO.Path.Combine(@".\DataConvertedWatch", fileName + ".txt");
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
                string imported_convert = (@".\Data_Converted.txt");
                using (var TextFile = new StreamWriter(imported_convert, true))
                {
                    string imported = files[numberOfLoops];
                    TextFile.WriteLine(imported);

                    TextFile.Close();

                }
                numberOfLoops++;
                //double progress = (double)numberOfLoops / (double)numberoOfFiles;
                //double progressDone = (double)progress * 100;
                //Form1.backgroundworkerConvertInstance.ReportProgress(Convert.ToInt32(progressDone));

            }
        }

        public static void pathPdfConvert(string path )
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
                        // Either extract based on order in the underlying document with newlines and spaces.
                        var text = ContentOrderTextExtractor.GetText(page);

                        // Or based on grouping letters into words.
                        // var otherText = string.Join(" ", page.GetWords());

                        // Or the raw text of the page's content stream.
                        //var text = page.Text;
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
