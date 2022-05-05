using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masuratori
{
    class sql_database_operation
    {
        public static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }
        public static void Add_fise(fise_class_table first)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString("Default")))
            {
                cnn.Execute("insert into fise(Numar,Reper,Data,Nume,Observatii)values (@Numar,@Reper,@Data,@Nume,@Observatii)", first);
                
            }
        }
        public static void Add_masuratori(masuratori_table first)
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
