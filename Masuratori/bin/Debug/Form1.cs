using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Data.SQLite;
using System.Timers;
using System.Reflection;



namespace Masuratori
{
    public partial class Form1 : Form
    {
        public static Form1 instance;
        public static ProgressBar progressBarInstance;
        public static BackgroundWorker backgroundworkerImportInstance;
        public static BackgroundWorker backgroundworkerConvertInstance;
        public static BackgroundWorker backgroundworkerWatchInstance;
        private System.Timers.Timer myTimer;

        public Form1()
        {
            InitializeComponent();
            instance = this;
            progressBarInstance = progressBar1;//instance for progresbar to use in other methods
            backgroundworkerImportInstance = backgroundWorker_import;//backgroundworker for sorting and importing converted pdf measurements
            backgroundworkerConvertInstance = backgroundWorker_convert;//backroundworker instance for converting pdf documents to text files
            backgroundworkerWatchInstance = backgroundWorker_watch;//backgroundworker to scan folder with pdf measurements to be converted
            
        }
        string pathToBeConverted { get; set; }
        private void button1_Click(object sender, EventArgs e)//button to convert pdf to text
        {

            FolderBrowserDialog fbd = new FolderBrowserDialog();//get path of folder with folder browser
            fbd.Description = "Custom Description";
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                pathToBeConverted = fbd.SelectedPath;
            }
            
            backgroundWorker_convert.ProgressChanged += backgroundWorker_convert_ProgressChanged;//progressbar
            backgroundWorker_convert.DoWork += new DoWorkEventHandler(backgroundWorker_convert_DoWork);//background event where we convert pdf to text file


            if (backgroundWorker_convert.IsBusy != true)
            {
                backgroundWorker_convert.RunWorkerAsync();

                backgroundWorker_convert.WorkerReportsProgress = true;
            }
        }

        static void SetDoubleBuffer(Control dataGridView1, bool DoubleBuffered)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                null, dataGridView1, new object[] { DoubleBuffered });
        }
        public void backgroundWorker_convert_DoWork(object sender, DoWorkEventArgs e)
        {
            convertPDF.pathPdfConvert(pathToBeConverted);//convert pdf to text
        }
        private void backgroundWorker_convert_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;//increase progressbar 
        }

        private void import_text_Click(object sender, EventArgs e)//button for importing and sorting the data to the database from the converted text files
        {
            backgroundWorker_import.ProgressChanged += backgroundWorker_import_ProgressChanged;
            backgroundWorker_import.DoWork += new DoWorkEventHandler(backgroundWorker_import_DoWork);//background event to import and sort the converted pdf text files

            if (backgroundWorker_import.IsBusy != true)
            {
                backgroundWorker_import.RunWorkerAsync();

                backgroundWorker_import.WorkerReportsProgress = true;
            }

        }
        public void backgroundWorker_import_DoWork(object sender, DoWorkEventArgs e)
        {
            Import.importBackgroundWork();//sort text files into types of measurement and import to database
        }
        private void backgroundWorker_import_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private void searchButton_Click(object sender, EventArgs e)//searching button from the database into a datagridview
        {
            SetDoubleBuffer(dataGridView1,true);
            string reper = textBox_reper.Text;
            string data = textBox_data.Text;
            string nume = textBox_nume.Text;
            string cota = textBox_cota.Text;
            string Nominal =textBox_ax.Text;
            string isOuttol = textBox_isOuttol.Text;
            if (!String.IsNullOrEmpty(data) | !String.IsNullOrEmpty(reper) | !String.IsNullOrEmpty(nume) | !String.IsNullOrEmpty(cota) | !String.IsNullOrEmpty(Nominal) | !String.IsNullOrEmpty(isOuttol))
            {
                using (IDbConnection cnn = new SQLiteConnection(sql_database_operation.LoadConnectionString("Default")))//using the default connection string connect to the database and execute the sql command
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Reper,Data,Nume,Observatii,Cota, Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol FROM fise INNER JOIN Masuratori on Masuratori.Fisa_id = Numar  WHERE fise.Data like " + "\"%" + data + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Reper like" + "\"%" + reper + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Nume like" + "\"%" + nume + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Cota like" + "\"%" + cota + "%\"" + "ESCAPE" + "\'\\'"+ "AND Masuratori.Nominal like" + "\"%" + Nominal + "%\"" + "ESCAPE" + "\'\\'" +"AND Masuratori.Is_outtol like" + "\"%" + isOuttol + "%\"", sql_database_operation.LoadConnectionString("Default"));
                    DataSet dset = new DataSet();
                    // MessageBox.Show("SELECT Numar,Reper,Data,Nume,Observatii,Fisa_id,Cota, Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol FROM fise INNER JOIN Masuratori on Masuratori.Fisa_id = Numar  WHERE fise.Data like " + "\"%" + data + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Reper like" + "\"%" + reper + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Nume like" + "\"%" + nume + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Cota like" + "\"%" + cota + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Ax like" + "\"%" + ax + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Is_outtol like" + "\"%" + isOuttol + "%\"");
                    adapter.Fill(dset, "info");
                    dataGridView1.DataSource = dset.Tables[0];
                }
            }
            if (String.IsNullOrEmpty(isOuttol))
            {
                using (IDbConnection cnn = new SQLiteConnection(sql_database_operation.LoadConnectionString("Default")))//using the default connection string connect to the database and execute the sql command
                {
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT Reper,Data,Nume,Observatii,Cota, Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol FROM fise INNER JOIN Masuratori on Masuratori.Fisa_id = Numar  WHERE fise.Data like " + "\"%" + data + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Reper like" + "\"%" + reper + "%\"" + "ESCAPE" + "\'\\'" + "AND fise.Nume like" + "\"%" + nume + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Cota like" + "\"%" + cota + "%\"" + "ESCAPE" + "\'\\'" + "AND Masuratori.Nominal like" + "\"%" + Nominal + "%\"" + "ESCAPE" + "\'\\'", sql_database_operation.LoadConnectionString("Default"));
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

        private void watcher_Click(object sender, EventArgs e)//a button to do the converting sorting and importing into database automaticaly at a set time interval
        {
            string[] pathPdf = File.ReadAllLines(@".\Path_of_measurements.txt");//text file that contains the path of the measurement pdf
            string pathPdfMeasurements = pathPdf[0];
            Cursor.Current = Cursors.WaitCursor;
            string[] files = Directory.GetFiles(pathPdfMeasurements, "*.pdf", SearchOption.AllDirectories);//search for all pdf in folder
            int scanning = files.Length;
            int scanned = 0;
            while (scanning != scanned)
            {
                string pathError = (@".\Data_to_be imported.txt");//create a list of what you have found
                using (var TextFile = new StreamWriter(pathError, true))
                {
                    string date = files[scanned];
                    TextFile.WriteLine(date);
                    TextFile.Close();
                }
                scanned++;
            }
            String[] linesA = File.ReadAllLines(@".\Data_to_be imported.txt");//check what you have found in pdf measurement folder
            String[] linesB = File.ReadAllLines(@".\Data_Converted.txt");//check what has been imported already

            IEnumerable<String> onlyB = linesA.Except(linesB);//compare what has to be imported and what is imported already

            File.WriteAllLines(@".\Data_compared.txt", onlyB);//write in a text file what is missing from the database and needs to be imported , use this file to import what is missing
            convertPDF.watcherPathPdfConvert();//convert pdf measurements to text and save the name of the PDF file to Data converted, this is so you have a list of what has been imported in database

            File.Delete(@".\Data_to_be imported.txt");//delete the compared list so that it resets each time and you dont import the same measurement more times
            Cursor.Current = Cursors.Default;
            backgroundWorker_watch.DoWork += new DoWorkEventHandler(backgroundWorker_watch_DoWork);//start sorting and importing the converted text files

            backgroundWorker_watch.ProgressChanged += backgroundWorker_watch_ProgressChanged;

            if (backgroundWorker_watch.IsBusy != true)
            {
                backgroundWorker_watch.RunWorkerAsync();

                backgroundWorker_watch.WorkerReportsProgress = true;
            }
            string[] importTime = File.ReadAllLines(@".\Timpul_de_importare_in_minute.txt");//text file that contains the timer to do the sorting and import automaticaly
            string importTimeinMinutes = importTime[0];//start a timer in which the importing is repeated at set time interval
            int timerMinutes = Int32.Parse(importTimeinMinutes) * 60000;//timer
            myTimer = new System.Timers.Timer();
            myTimer.Elapsed += new ElapsedEventHandler(automaticUpdate);
            myTimer.Interval = timerMinutes;
            myTimer.Enabled = true;

        }
        private void automaticUpdate(object source, ElapsedEventArgs e)//this is the same but  automatic importing done using the timer
        {
            
            string[] pathPdf = File.ReadAllLines(@".\Path_of_measurements.txt");
            string pathPdfMeasurements = pathPdf[0];
            string[] files = Directory.GetFiles(pathPdfMeasurements, "*.pdf", SearchOption.AllDirectories);
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
        
        public void backgroundWorker_watch_DoWork(object sender, DoWorkEventArgs e)//sorting and importing the converted measurements 
        {
            Import.importBackgroundWork_Watcher();
        }
        
        private void backgroundWorker_watch_ProgressChanged(object sender, ProgressChangedEventArgs e)//progressbar increment
        {
            Form1.progressBarInstance.Invoke((MethodInvoker)delegate
            {
                Form1.progressBarInstance.Value = e.ProgressPercentage;

            });
        }

    }
}
    
