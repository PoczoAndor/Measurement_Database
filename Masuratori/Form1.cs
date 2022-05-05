using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.IO;
using System.Data.SQLite;
using Dapper;
using System.Configuration;
using UglyToad.PdfPig;
using UglyToad.PdfPig.DocumentLayoutAnalysis.TextExtractor;
using System.Threading;

namespace Masuratori
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public static ProgressBar progressBarInstance;
        public static BackgroundWorker backgroundworkerImportInstance;
        public static BackgroundWorker backgroundworkerConvertInstance;
        public static BackgroundWorker backgroundworkerWatchInstance;
        public Form1()
        {
            InitializeComponent();
            instance = this;
            progressBarInstance = progressBar1;
            backgroundworkerImportInstance = backgroundWorker_import;
            backgroundworkerConvertInstance = backgroundWorker_convert;
            backgroundworkerWatchInstance = backgroundWorker_watch;
        }
        string StringA { get; set; }
        private void button1_Click(object sender, EventArgs e)//button to convert pdf to text
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                StringA = fbd.SelectedPath;
            }
            
            backgroundWorker_convert.ProgressChanged += backgroundWorker_convert_ProgressChanged;
            backgroundWorker_convert.DoWork += new DoWorkEventHandler(backgroundWorker_convert_DoWork);


            if (backgroundWorker_convert.IsBusy != true)
            {
                backgroundWorker_convert.RunWorkerAsync();

                backgroundWorker_convert.WorkerReportsProgress = true;
            }



        }
        public void backgroundWorker_convert_DoWork(object sender, DoWorkEventArgs e)
        {


            convertPDF.pathPdfConvert(StringA);


        }
        private void backgroundWorker_convert_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void import_text_Click(object sender, EventArgs e)//importing the data to the database
        {


            //Start the asynchronous operation.
            //backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            backgroundWorker_import.ProgressChanged += backgroundWorker_import_ProgressChanged;
            backgroundWorker_import.DoWork += new DoWorkEventHandler(backgroundWorker_import_DoWork);


            if (backgroundWorker_import.IsBusy != true)
            {
                backgroundWorker_import.RunWorkerAsync();

                backgroundWorker_import.WorkerReportsProgress = true;
            }


        }
        public void backgroundWorker_import_DoWork(object sender, DoWorkEventArgs e)
        {
            Import.importBackgroundWork();
        }
        private void backgroundWorker_import_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }




        private void searchButton_Click(object sender, EventArgs e)//searching button
        {
            string reper = textBox_reper.Text;
            string data = textBox_data.Text;
            string nume = textBox_nume.Text;
            string cota = textBox_cota.Text;
            string Nominal =textBox_ax.Text;
            string isOuttol = textBox_isOuttol.Text;
            if (!String.IsNullOrEmpty(data) | !String.IsNullOrEmpty(reper) | !String.IsNullOrEmpty(nume) | !String.IsNullOrEmpty(cota) | !String.IsNullOrEmpty(Nominal) | !String.IsNullOrEmpty(isOuttol))
            {
                using (IDbConnection cnn = new SQLiteConnection(sql_database_operation.LoadConnectionString("Default")))
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Numar,Reper,Data,Nume,Observatii,Fisa_id,Cota, Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol FROM fise INNER JOIN Masuratori on Masuratori.Fisa_id = Numar  WHERE fise.Data like " + "\"%" + data + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Reper like" + "\"%" + reper + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Nume like" + "\"%" + nume + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Cota like" + "\"%" + cota + "%\"" + "ESCAPE" + "\'\\'"+ "AND Masuratori.Nominal like" + "\"%" + Nominal + "%\"" + "ESCAPE" + "\'\\'" +"AND Masuratori.Is_outtol like" + "\"%" + isOuttol + "%\"", sql_database_operation.LoadConnectionString("Default"));
                    DataSet dset = new DataSet();
                    // MessageBox.Show("SELECT Numar,Reper,Data,Nume,Observatii,Fisa_id,Cota, Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol FROM fise INNER JOIN Masuratori on Masuratori.Fisa_id = Numar  WHERE fise.Data like " + "\"%" + data + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Reper like" + "\"%" + reper + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Nume like" + "\"%" + nume + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Cota like" + "\"%" + cota + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Ax like" + "\"%" + ax + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Is_outtol like" + "\"%" + isOuttol + "%\"");
                    adapter.Fill(dset, "info");
                    dataGridView1.DataSource = dset.Tables[0];
                }
            }
        }



        private void button_save_excel_Click(object sender, EventArgs e)//save the info from datagrid to excel file
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Documents (*.xls)|*.xls";
            sfd.FileName = "export.xls";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
               
                saveExcel.ToCsV(dataGridView1, sfd.FileName);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void watcher_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Custom Description";
            string path="";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                path = fbd.SelectedPath;

            }
            
                //MessageBox.Show(path);
                string[] files = Directory.GetFiles(path, "*.pdf", SearchOption.AllDirectories);
                int scanning = files.Length;
                int scanned = 0;
                while (scanning != scanned)
                {
                    string pathError = (@".\Data_to_be imported.txt");
                    using (var TextFile = new StreamWriter(pathError, true))
                    {
                        string date = files[scanned];
                        TextFile.WriteLine(date);

                        TextFile.Close();

                    }
                    scanned++;
                }
                String[] linesA = File.ReadAllLines(@".\Data_to_be imported.txt");
                String[] linesB = File.ReadAllLines(@".\Data_Converted.txt");

                IEnumerable<String> onlyB = linesA.Except(linesB);

                File.WriteAllLines(@".\Data_compared.txt", onlyB);
                convertPDF.watcherPathPdfConvert();

                File.Delete(@".\Data_to_be imported.txt");
                
                backgroundWorker_watch.DoWork += new DoWorkEventHandler(backgroundWorker_watch_DoWork);
                backgroundWorker_watch.ProgressChanged += backgroundWorker_watch_ProgressChanged;

                if (backgroundWorker_watch.IsBusy != true)
                {
                    backgroundWorker_watch.RunWorkerAsync();

                    backgroundWorker_watch.WorkerReportsProgress = true;
                }

          
            



        }
        public void backgroundWorker_watch_DoWork(object sender, DoWorkEventArgs e)
        {
            Import.importBackgroundWork_Watcher();
        }
        //private delegate void ToDoDelegate();
        private void backgroundWorker_watch_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ////this.progressBar1.AutoInvoke(() => this.ProgressBar1.Value = start);
            //Invoke(new ToDoDelegate(() => progressBar1.Value = e.ProgressPercentage));
            Form1.progressBarInstance.Value = e.ProgressPercentage;
        }

    }
}
    
