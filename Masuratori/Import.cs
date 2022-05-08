using Dapper;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace Masuratori
{
    class Import
    {

        public static void importBackgroundWork()//importing from text file and sorting trough the data without a set time interval
        {

            string[] filesConverted = Directory.GetFiles(@".\DataConverted", "*.txt", SearchOption.AllDirectories);
            masuratori_table b = new masuratori_table();
            fise_class_table fiseTable = new fise_class_table();
            string lastID;
            using (IDbConnection cnn = new SQLiteConnection(sql_database_operation.LoadConnectionString("Default")))
            {
                lastID = cnn.ExecuteScalar("SELECT max(Numar) FROM fise;").ToString();
            }

            int id = Int32.Parse(lastID) + 1;
            int numberOfFiles = filesConverted.Length;
            int numberOfFilesDone = 0;


            while (numberOfFiles != numberOfFilesDone)//loop trought folder and get all the converted text files
            {
                try
                {
                    fiseTable.Numar = id.ToString();
                    id++;

                    string[] readText = File.ReadAllLines(filesConverted[numberOfFilesDone]);
                    int lenghtOfText = readText.Length;
                    int counter = 0;

                    fiseTable.Reper = readText[0].ToString();//reper
                    fiseTable.Data = readText[1].ToString();//data
                    fiseTable.Nume = readText[2].ToString();//filename

                    if (readText[3] == "ZEISS  Calypso")//if measurement file is of type ziess
                    {
                        b.Fisa_id = fiseTable.Numar;
                        fiseTable.Observatii = readText[5].ToString();
                        sql_database_operation.Add_fise(fiseTable);
                        int counterZeiss = 11;
                        while (counterZeiss != lenghtOfText)
                        {
                            if (!readText[counterZeiss].Contains("!") & !readText[counterZeiss].Contains("Plan Name Operator Time Date") & !readText[counterZeiss].Contains(":") & !readText[counterZeiss].Contains("Lower Tol.Upper Tol.NominalActual Deviation") & readText[counterZeiss].Length != 0)
                            {
                                string[] measurementValuesIn_a_row_zeiss = readText[counterZeiss].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                if (measurementValuesIn_a_row_zeiss.Length == 2)//get cota name
                                {


                                    b.Cota = measurementValuesIn_a_row_zeiss[0].ToString();
                                    if (!readText[counterZeiss].Contains("|"))//if it is outtol
                                    {

                                        b.Outtol = measurementValuesIn_a_row_zeiss[1].ToString();
                                        b.Directia = null;
                                        b.Is_outtol = "DA";
                                    }
                                    if (readText[counterZeiss].Contains("|"))//if it is not outtol
                                    {
                                        b.Outtol = null;
                                        b.Directia = measurementValuesIn_a_row_zeiss[1].ToString();
                                        b.Is_outtol = "NU";
                                    }
                                }
                                if (measurementValuesIn_a_row_zeiss.Length == 3)//get cota name if they put space in name
                                {


                                    b.Cota = measurementValuesIn_a_row_zeiss[0].ToString() + measurementValuesIn_a_row_zeiss[1].ToString();
                                    if (!readText[counterZeiss].Contains("|"))//if it is outtol
                                    {

                                        b.Outtol = measurementValuesIn_a_row_zeiss[2].ToString();
                                        b.Directia = null;
                                        b.Is_outtol = "DA";
                                    }
                                    if (readText[counterZeiss].Contains("|"))//if it is not outtol
                                    {
                                        b.Outtol = null;
                                        b.Directia = measurementValuesIn_a_row_zeiss[2].ToString();
                                        b.Is_outtol = "NU";
                                    }
                                }


                                if (!readText[counterZeiss].Contains("Value") & measurementValuesIn_a_row_zeiss[0] == "X" | measurementValuesIn_a_row_zeiss[0] == "Z" | measurementValuesIn_a_row_zeiss[0] == "Y")//if its position x y z
                                {

                                    b.Ax = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Nominal = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_plus = null;
                                    b.Tol_minus = null;
                                    b.Bonus = null;



                                    sql_database_operation.Add_masuratori(b);
                                }
                                if (measurementValuesIn_a_row_zeiss[0] != "X" & measurementValuesIn_a_row_zeiss[0] != "Y" & measurementValuesIn_a_row_zeiss[0] != "Z" & measurementValuesIn_a_row_zeiss.Length == 4)//if its not position xyz but position value
                                {
                                    b.Ax = null;
                                    b.Nominal = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Tol_plus = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_minus = null;
                                    b.Bonus = null;


                                    sql_database_operation.Add_masuratori(b);
                                }
                                if (measurementValuesIn_a_row_zeiss[0] != "x" & measurementValuesIn_a_row_zeiss[0] != "y" & measurementValuesIn_a_row_zeiss[0] != "z" & measurementValuesIn_a_row_zeiss.Length == 5)//any other type of measurement
                                {

                                    b.Ax = null;
                                    b.Nominal = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[4].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Tol_plus = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_minus = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Bonus = null;



                                    sql_database_operation.Add_masuratori(b);

                                }
                            }
                            counterZeiss++;
                        }
                    }
                    else //if measurement report is not of type ziess then sort it acording to type pc dmis
                    {

                        if (readText[3].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = readText[3].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[4].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[4].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[5].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[5].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[6].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[6].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[7].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[7].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[8].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = readText[8].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (!readText[8].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = null;
                            sql_database_operation.Add_fise(fiseTable);
                        }


                        while (counter != lenghtOfText)//start sorting measurement types 
                        {

                            if ((!readText[counter].Contains("!") & readText[counter].Contains("UNITS") | readText[counter].Contains("DIM") | readText[counter].Contains("POSITION :")))//printing the dimension name
                            {
                                string content = readText[counter];
                                //int foreignKey = numberOfFilesDone + 1;
                                b.Fisa_id = fiseTable.Numar;
                                b.Cota = content;

                            }
                            if ((!readText[counter].Contains("UNITS=MM") & !readText[counter].Contains("DIM") & !readText[counter].Contains("AX") & !readText[counter].Contains("Matrita") & !readText[counter].Contains("``") & !readText[counter].Contains("Active alignment changed") & !readText[counter].Contains("Alignment Recalled") & !readText[counter].Contains("of")))//printing the dimension name``
                            {

                                string[] measurementValuesIn_a_row = readText[counter].Split();//split a row up into cell values

                                if (!readText[counter].Contains("D1") & !readText[counter].Contains("D2") & !readText[counter].Contains("D3") & !readText[counter].Contains("D4")) //leave out anything with d1 measurements
                                {


                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 8 & measurementValuesIn_a_row.Length > 4 & readText[counter].Contains("TP"))//if the measurements is a position
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Bonus = measurementValuesIn_a_row[4].ToString();
                                        b.Dev = measurementValuesIn_a_row[5].ToString();
                                        b.Outtol = measurementValuesIn_a_row[6].ToString();
                                        b.Directia = measurementValuesIn_a_row[7].ToString();
                                        b.Tol_minus = null;
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }


                                        sql_database_operation.Add_masuratori(b);


                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 10 & measurementValuesIn_a_row.Length > 4 & readText[counter].Contains("TP") & readText[counter].Contains("HIT:"))//if the measurements is a position with d1 d2
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Bonus = measurementValuesIn_a_row[6].ToString();
                                        b.Dev = measurementValuesIn_a_row[7].ToString();
                                        b.Outtol = measurementValuesIn_a_row[8].ToString();
                                        b.Directia = measurementValuesIn_a_row[9].ToString();
                                        b.Tol_minus = null;
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }


                                        sql_database_operation.Add_masuratori(b);


                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length > 4 & measurementValuesIn_a_row.Length == 9) //if there is a bonus in the measurement
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = measurementValuesIn_a_row[4].ToString();
                                        b.Bonus = measurementValuesIn_a_row[5].ToString();
                                        b.Dev = measurementValuesIn_a_row[6].ToString();
                                        b.Outtol = measurementValuesIn_a_row[7].ToString();
                                        b.Directia = measurementValuesIn_a_row[8].ToString();
                                        if (measurementValuesIn_a_row[7] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[7]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[7] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[7]))
                                        {
                                            b.Is_outtol = "DA";
                                        }
                                        sql_database_operation.Add_masuratori(b);
                                    }

                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 4 & measurementValuesIn_a_row.Length != 9 & !readText[counter].Contains("TP"))//axis with only deviation no tolerance
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Dev = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = null;
                                        b.Bonus = null;
                                        b.Dev = null;
                                        b.Outtol = null;
                                        b.Directia = null;
                                        b.Is_outtol = null;
                                        sql_database_operation.Add_masuratori(b);
                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length != 4 & measurementValuesIn_a_row.Length > 4 & measurementValuesIn_a_row.Length != 9 & !readText[counter].Contains("TP") & !readText[counter].Contains("EXCEL_FORM1 = NOT_USED BUILT FROM FEATURE EXCEL_FORM1") & !readText[counter].Contains("POSITION :"))// normal measurements
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = measurementValuesIn_a_row[4].ToString();
                                        b.Bonus = null;
                                        b.Dev = measurementValuesIn_a_row[5].ToString();
                                        b.Outtol = measurementValuesIn_a_row[6].ToString();
                                        b.Directia = measurementValuesIn_a_row[7].ToString();
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }
                                        sql_database_operation.Add_masuratori(b);

                                    }
                                }

                            }
                            counter++;
                        }
                    }
                }
                catch (System.IndexOutOfRangeException error)//if it cannot be imported then write in a text file where it failed
                {

                    string pathError = (@".\DataNotConverted.txt");
                    using (var TextFile = new StreamWriter(pathError, true))
                    {
                        string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
                        TextFile.WriteLine(date);
                        TextFile.WriteLine(filesConverted[numberOfFilesDone]);
                        TextFile.WriteLine(error);
                        TextFile.Close();

                    }

                }

                numberOfFilesDone++;
                double progress = (double)numberOfFilesDone / (double)numberOfFiles;
                double progressDone = (double)progress * 100;
                Form1.backgroundworkerImportInstance.ReportProgress(Convert.ToInt32(progressDone));//progresbbar on from 1

            }

        }
        public static void importBackgroundWork_Watcher()//do importing automaticaly at a set time interval
        {

            string[] filesConverted = Directory.GetFiles(@".\DataConvertedWatch", "*.txt", SearchOption.AllDirectories);
            masuratori_table b = new masuratori_table();
            fise_class_table fiseTable = new fise_class_table();
            string lastID;
            using (IDbConnection cnn = new SQLiteConnection(sql_database_operation.LoadConnectionString("Default")))
            {
                lastID = cnn.ExecuteScalar("SELECT max(Numar) FROM fise;").ToString();
            }

            int id = Int32.Parse(lastID) + 1;
            int numberOfFiles = filesConverted.Length;
            int numberOfFilesDone = 0;


            while (numberOfFiles != numberOfFilesDone)//loop trought folder and get all the converted text files
            {
                try
                {
                    fiseTable.Numar = id.ToString();
                    id++;

                    string[] readText = File.ReadAllLines(filesConverted[numberOfFilesDone]);
                    int lenghtOfText = readText.Length;
                    int counter = 0;

                    fiseTable.Reper = readText[0].ToString();//reper
                    fiseTable.Data = readText[1].ToString();//data
                    fiseTable.Nume = readText[2].ToString();//filename

                    if (readText[3] == "ZEISS  Calypso")//if measurement file is of type ziess
                    {
                        b.Fisa_id = fiseTable.Numar;
                        fiseTable.Observatii = readText[5].ToString();
                        sql_database_operation.Add_fise(fiseTable);
                        int counterZeiss = 11;
                        while (counterZeiss != lenghtOfText)
                        {
                            if (!readText[counterZeiss].Contains("!") & !readText[counterZeiss].Contains("Plan Name Operator Time Date") & !readText[counterZeiss].Contains(":") & !readText[counterZeiss].Contains("Lower Tol.Upper Tol.NominalActual Deviation") & readText[counterZeiss].Length != 0)
                            {
                                string[] measurementValuesIn_a_row_zeiss = readText[counterZeiss].Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                if (measurementValuesIn_a_row_zeiss.Length == 2)//get cota name
                                {


                                    b.Cota = measurementValuesIn_a_row_zeiss[0].ToString();
                                    if (!readText[counterZeiss].Contains("|"))//if it is outtol
                                    {

                                        b.Outtol = measurementValuesIn_a_row_zeiss[1].ToString();
                                        b.Directia = null;
                                        b.Is_outtol = "DA";
                                    }
                                    if (readText[counterZeiss].Contains("|"))//if it is not outtol
                                    {
                                        b.Outtol = null;
                                        b.Directia = measurementValuesIn_a_row_zeiss[1].ToString();
                                        b.Is_outtol = "NU";
                                    }
                                }
                                if (measurementValuesIn_a_row_zeiss.Length == 3)//get cota name if they put space in name
                                {


                                    b.Cota = measurementValuesIn_a_row_zeiss[0].ToString() + measurementValuesIn_a_row_zeiss[1].ToString();
                                    if (!readText[counterZeiss].Contains("|"))//if it is outtol
                                    {

                                        b.Outtol = measurementValuesIn_a_row_zeiss[2].ToString();
                                        b.Directia = null;
                                        b.Is_outtol = "DA";
                                    }
                                    if (readText[counterZeiss].Contains("|"))//if it is not outtol
                                    {
                                        b.Outtol = null;
                                        b.Directia = measurementValuesIn_a_row_zeiss[2].ToString();
                                        b.Is_outtol = "NU";
                                    }
                                }


                                if (!readText[counterZeiss].Contains("Value") & measurementValuesIn_a_row_zeiss[0] == "X" | measurementValuesIn_a_row_zeiss[0] == "Z" | measurementValuesIn_a_row_zeiss[0] == "Y")//if its position x y z
                                {

                                    b.Ax = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Nominal = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_plus = null;
                                    b.Tol_minus = null;
                                    b.Bonus = null;



                                    sql_database_operation.Add_masuratori(b);
                                }
                                if (measurementValuesIn_a_row_zeiss[0] != "X" & measurementValuesIn_a_row_zeiss[0] != "Y" & measurementValuesIn_a_row_zeiss[0] != "Z" & measurementValuesIn_a_row_zeiss.Length == 4)//if its not position xyz but position value
                                {
                                    b.Ax = null;
                                    b.Nominal = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Tol_plus = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_minus = null;
                                    b.Bonus = null;


                                    sql_database_operation.Add_masuratori(b);
                                }
                                if (measurementValuesIn_a_row_zeiss[0] != "x" & measurementValuesIn_a_row_zeiss[0] != "y" & measurementValuesIn_a_row_zeiss[0] != "z" & measurementValuesIn_a_row_zeiss.Length == 5)//any other type of measurement
                                {

                                    b.Ax = null;
                                    b.Nominal = measurementValuesIn_a_row_zeiss[3].ToString();
                                    b.Meas = measurementValuesIn_a_row_zeiss[4].ToString();
                                    b.Dev = measurementValuesIn_a_row_zeiss[2].ToString();
                                    b.Tol_plus = measurementValuesIn_a_row_zeiss[1].ToString();
                                    b.Tol_minus = measurementValuesIn_a_row_zeiss[0].ToString();
                                    b.Bonus = null;



                                    sql_database_operation.Add_masuratori(b);

                                }
                            }
                            counterZeiss++;
                        }
                    }
                    else //if converted text file is not of type ziess look for info on the part measured
                    {

                        if (readText[3].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = readText[3].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[4].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[4].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[5].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[5].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[6].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[6].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[7].Contains("Matrita:"))
                        {
                            fiseTable.Observatii = readText[7].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (readText[8].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = readText[8].ToString();
                            sql_database_operation.Add_fise(fiseTable);
                        }
                        else if (!readText[8].Contains("Matrita:"))
                        {

                            fiseTable.Observatii = null;
                            sql_database_operation.Add_fise(fiseTable);
                        }


                        while (counter != lenghtOfText)//start sorting measurement types 
                        {

                            if ((!readText[counter].Contains("!") & readText[counter].Contains("UNITS") | readText[counter].Contains("DIM") | readText[counter].Contains("POSITION :")))//printing the dimension name
                            {
                                string content = readText[counter];
                                //int foreignKey = numberOfFilesDone + 1;
                                b.Fisa_id = fiseTable.Numar;
                                b.Cota = content;

                            }
                            if ((!readText[counter].Contains("UNITS=MM") & !readText[counter].Contains("DIM") & !readText[counter].Contains("AX") & !readText[counter].Contains("Matrita") & !readText[counter].Contains("``") & !readText[counter].Contains("Active alignment changed") & !readText[counter].Contains("Alignment Recalled") & !readText[counter].Contains("of")))//printing the dimension name``
                            {

                                string[] measurementValuesIn_a_row = readText[counter].Split();//split a row up into cell values

                                if (!readText[counter].Contains("D1") & !readText[counter].Contains("D2") & !readText[counter].Contains("D3") & !readText[counter].Contains("D4")) //leave out anything with d1 measurements
                                {


                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 8 & measurementValuesIn_a_row.Length > 4 & readText[counter].Contains("TP"))//if the measurements is a position
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Bonus = measurementValuesIn_a_row[4].ToString();
                                        b.Dev = measurementValuesIn_a_row[5].ToString();
                                        b.Outtol = measurementValuesIn_a_row[6].ToString();
                                        b.Directia = measurementValuesIn_a_row[7].ToString();
                                        b.Tol_minus = null;
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }


                                        sql_database_operation.Add_masuratori(b);


                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 10 & measurementValuesIn_a_row.Length > 4 & readText[counter].Contains("TP") & readText[counter].Contains("HIT:"))//if the measurements is a position with d1 d2
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Bonus = measurementValuesIn_a_row[6].ToString();
                                        b.Dev = measurementValuesIn_a_row[7].ToString();
                                        b.Outtol = measurementValuesIn_a_row[8].ToString();
                                        b.Directia = measurementValuesIn_a_row[9].ToString();
                                        b.Tol_minus = null;
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }


                                        sql_database_operation.Add_masuratori(b);


                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length > 4 & measurementValuesIn_a_row.Length == 9) //if there is a bonus in the measurement
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = measurementValuesIn_a_row[4].ToString();
                                        b.Bonus = measurementValuesIn_a_row[5].ToString();
                                        b.Dev = measurementValuesIn_a_row[6].ToString();
                                        b.Outtol = measurementValuesIn_a_row[7].ToString();
                                        b.Directia = measurementValuesIn_a_row[8].ToString();
                                        if (measurementValuesIn_a_row[7] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[7]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[7] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[7]))
                                        {
                                            b.Is_outtol = "DA";
                                        }
                                        sql_database_operation.Add_masuratori(b);
                                    }

                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length == 4 & measurementValuesIn_a_row.Length != 9 & !readText[counter].Contains("TP"))//axis with only deviation no tolerance
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Dev = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = null;
                                        b.Bonus = null;
                                        b.Dev = null;
                                        b.Outtol = null;
                                        b.Directia = null;
                                        b.Is_outtol = null;
                                        sql_database_operation.Add_masuratori(b);
                                    }
                                    if (!readText[counter].Contains("!") & measurementValuesIn_a_row.Length != 4 & measurementValuesIn_a_row.Length > 4 & measurementValuesIn_a_row.Length != 9 & !readText[counter].Contains("TP") & !readText[counter].Contains("EXCEL_FORM1 = NOT_USED BUILT FROM FEATURE EXCEL_FORM1") & !readText[counter].Contains("POSITION :"))// normal measurements
                                    {
                                        b.Ax = measurementValuesIn_a_row[0].ToString();
                                        b.Nominal = measurementValuesIn_a_row[1].ToString();
                                        b.Meas = measurementValuesIn_a_row[2].ToString();
                                        b.Tol_plus = measurementValuesIn_a_row[3].ToString();
                                        b.Tol_minus = measurementValuesIn_a_row[4].ToString();
                                        b.Bonus = null;
                                        b.Dev = measurementValuesIn_a_row[5].ToString();
                                        b.Outtol = measurementValuesIn_a_row[6].ToString();
                                        b.Directia = measurementValuesIn_a_row[7].ToString();
                                        if (measurementValuesIn_a_row[6] == "0.000" | measurementValuesIn_a_row[6] == "0.0000" | String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "NU";
                                        }
                                        else if (measurementValuesIn_a_row[6] != "0.000" | measurementValuesIn_a_row[6] != "0.0000" | !String.IsNullOrEmpty(measurementValuesIn_a_row[6]))
                                        {
                                            b.Is_outtol = "DA";
                                        }
                                        sql_database_operation.Add_masuratori(b);

                                    }
                                }

                            }
                            counter++;
                        }
                    }
                }
                catch (System.IndexOutOfRangeException error)
                {

                    string pathError = (@".\DataNotConverted.txt");//if you didnt manage to import write it in a text file
                    using (var TextFile = new StreamWriter(pathError, true))
                    {
                        string date = DateTime.UtcNow.ToString("MM-dd-yyyy");
                        TextFile.WriteLine(date);
                        TextFile.WriteLine(filesConverted[numberOfFilesDone]);
                        TextFile.WriteLine(error);
                        TextFile.Close();

                    }

                }
                try
                {
                    File.Delete(filesConverted[numberOfFilesDone]);
                }
                catch (System.IndexOutOfRangeException)
                { }
                numberOfFilesDone++;
                
                double progress = (double)numberOfFilesDone / (double)numberOfFiles;
                double progressDone = (double)progress * 100;
                Form1.backgroundworkerWatchInstance.ReportProgress(Convert.ToInt32(progressDone));

            }

        }


    }


}

