using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace Masuratori
{
    class sql_database_operation
    {
        public static string LoadConnectionString(string id = "Default")//create a connectionstring to sqlite database
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }
        public static void Add_fise(fise_class_table first)//import data into first sqlite table containing information about the pdf file
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("Default")))
            {
                cnn.Execute("insert into fise(Numar,Reper,Data,Nume,Observatii)values (@Numar,@Reper,@Data,@Nume,@Observatii)", first);
                
            }
        }
        public static void Add_masuratori(masuratori_table first)//import data into the second table containing all the measurements
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("Default")))
            {
                cnn.Execute("insert into Masuratori(Fisa_id,Cota,Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol)values (@Fisa_id,@Cota,@Ax,@Nominal,@Meas,@Tol_plus,@Tol_minus,@Bonus,@Dev,@Outtol,@Directia,@Is_outtol)", first);

            }
        }
        public static void View_masuratori(masuratori_table first)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("Default")))
            {
                cnn.Execute("insert into Masuratori(Fisa_id,Cota,Ax,Nominal,Meas,Tol_plus,Tol_minus,Bonus,Dev,Outtol,Directia,Is_outtol)values (@Fisa_id,@Cota,@Ax,@Nominal,@Meas,@Tol_plus,@Tol_minus,@Bonus,@Dev,@Outtol,@Directia,@Is_outtol)", first);

            }
        }
       
    }
}
