using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Windows;

namespace IO
    {
        public class FuntionsDB
        {
            private SqlConnection myConnection;
            private string _ConnectionString;
            public FuntionsDB()
            {
            }

            /// <summary>
            /// Property der holder de fornødne informationer der sætter SqlConnection 
            /// i stand til at oprette forbindelse til en given SqlServer
            /// </summary>
            protected string ConnectionString
            {
                get
                {
                    return _ConnectionString;
                }
                set
                {
                    if (value != ConnectionString)
                    {
                        _ConnectionString = value;
                        myConnection = new SqlConnection(_ConnectionString);
                    }
                }
            }
            /// <summary>
            /// Denne metode åbner forbindelsen til databasen
            /// Hvis der ikke er angivet en gyldig ConString, får brugeren en fejlmeddelse
            /// Hvis der er en åben forbindelse i forvejen, lukkes denne og der foretages et rekursivt kald
            /// </summary>
            private void OpenDB()
            {
                try
                {
                    if (myConnection != null && myConnection.State == ConnectionState.Closed)
                    {
                        myConnection.Open();
                    }
                    else
                    {
                        if (myConnection == null)
                        {
                            MessageBox.Show("Der er ikke angivet nogen, eller\nder er fejl i den angivne 'ConnectionString'");
                        }
                        else
                        {
                            CloseDB();
                            OpenDB();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Denne metode lukker forbindelsen til databasen
            /// </summary>
            private void CloseDB()
            {
                try
                {
                    myConnection.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Funktionen DbReturnDataTable, skal returnere 
            /// datatypen DataTable og modtage et SQL-Udtry i vores strSql string.
            /// Vi opretter en ny instans af DataTable og navngiver den dtRes og åbner forbindelsen til DB.
            /// Derefter fortæller vi vores DB hvilke informationer den skal hive ud via vores strSql og 
            /// indsætter resultatsættet i vores dtRes.
            /// Til slut lukker vi DB igen.
            /// </summary>
            /// <param name="strSql">Tekst string som indeholder vores Sql udtryk.</param>
            /// <returns>DataTable med det samlede resultatsæt.</returns>
            protected DataTable DbReturnDataTable(string strSql)
            {
                DataTable dtRes = new DataTable();
                try
                {
                    OpenDB();
                    // command er en instans af SqlCommand som er initialiseret med 
                    // strSql (Sql udtrykket) og myConnection. 
                    using (SqlCommand command = new SqlCommand(strSql, myConnection))
                    // adapter er en instans af SqlDataAdapter som er initialiseret til at
                    // håndtere de data som command vil returnere ved eksekvering.
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // adapter overfører data til dtRes som er af datatypen DataTable
                        adapter.Fill(dtRes);
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                return dtRes;
            }

            /// <summary>
            /// Funktionen DbReturnDataTable, skal returnere 
            /// datatypen DataSet og modtage et SQL-Udtry i vores strSql string.
            /// Vi opretter en ny instans af DataSet og navngiver den dsRes og åbner forbindelsen til DB.
            /// Derefter fortæller vi vores DB hvilke informationer den skal hive ud via vores strSql og 
            /// indsætter resultatsættet i vores dsRes.
            /// Til slut lukker vi DB igen.
            /// </summary>
            /// <param name="strSql">Tekst string som indeholder vores Sql udtryk.</param>
            /// <returns>DataSet med det samlede resultatsæt.</returns>
            protected DataSet DbReturnDataSet(string strSql)
            {
                DataSet dsRes = new DataSet();
                try
                {
                    OpenDB();
                    // command er en instans af SqlCommand som er initialiseret med 
                    // strSql (Sql udtrykket) og myConnection. 
                    using (SqlCommand command = new SqlCommand(strSql, myConnection))
                    // adapter er en instans af SqlDataAdapter som er initialiseret til at
                    // håndtere de data som command vil returnere ved eksekvering.
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        // adapter overfører data til dsRes som er af datatypen DataTable
                        adapter.Fill(dsRes);
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {

                    throw ex;
                }
                return dsRes;
            }

            /// <summary>
            /// Denne funktion returnerer en string, 
            /// der modsvarer strSql forespørgelsen.
            /// </summary>
            /// <param name="strSql">Tekststreng med SQL-udtrykket</param>
            /// <returns>String</returns>
            protected string DbReturnString(string strSql)
            {
                string strRes = "";

                try
                {
                    OpenDB();
                    using (SqlCommand cmd = new SqlCommand(strSql, myConnection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            strRes = reader.GetValue(0).ToString();
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                return strRes;
            }

            /// <summary>
            /// Denne metode returnere en row, som er conveteret til string, 
            /// som bliver stoppet i en list.
            /// - Vore strSql Afgøre hvilke row der bliver valgt, 
            /// og herefter skriver vi hvert field in i strList.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            /// <returns name="strList">List af strings</returns>
            protected List<string> DbReturnDbRowAsListString(string strSql)
            {
                List<string> strList = new List<string>();
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = myConnection.CreateCommand())
                    {
                        cmd.CommandText = strSql;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    strList.Add(reader.GetValue(i).ToString());
                                }
                            }
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;

                }
                return strList;
            }


            /// <summary>
            /// Denne metode returnere en collum, som er conveteret til string, 
            /// som bliver stoppet i en list.
            /// - Vore strSql Afgøre hvilke collum der bliver valgt, 
            /// og herefter skriver vi hvert field in i strList.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            /// <returns name="strList">List af strings</returns>
            protected List<string> DbReturnDbCollumAsListString(string strSql)
            {
                List<string> strList = new List<string>();
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = myConnection.CreateCommand())
                    {
                        cmd.CommandText = strSql;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                strList.Add(reader.GetValue(0).ToString());

                            }
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;

                }
                return strList;
            }

            /// <summary>
            /// Denne metode returnere en row, som er conveteret til string, 
            /// som bliver stoppet i en ArrayList.
            /// - Vore strSql Afgøre hvilke row der bliver valgt, 
            /// og herefter skriver vi hvert field in i arrayList.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            /// <returns name="arrayList">Array List af data</returns>
            protected ArrayList DBReturnDBRowAsArrayList(string strSql)
            {
                ArrayList arrayList = new ArrayList();
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = myConnection.CreateCommand())
                    {
                        cmd.CommandText = strSql;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    arrayList.Add(reader.GetValue(i).ToString());
                                }
                            }
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return arrayList;
            }

            /// <summary>
            /// Denne metode returnere en Collum, som bliver stoppet i en ArrayList.
            /// - Vore strSql Afgøre hvilke Collum der bliver valgt, 
            /// og herefter skriver vi hvert field in i arrayList.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            /// <returns name="arrayList">Array List af data</returns>
            protected ArrayList DBReturnDBCollumAsArrayList(string strSql)
            {
                ArrayList arrayList = new ArrayList();
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = myConnection.CreateCommand())
                    {
                        cmd.CommandText = strSql;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                arrayList.Add(reader.GetValue(0));
                            }
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return arrayList;
            }

            /// <summary>
            /// Denne metode bliver brugt til at skrive ned i DB,
            /// Uden at den forventer at få noget tilbage.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            protected void FunctionExecuteNonQuery(string strSql)
            {
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = new SqlCommand(strSql, myConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            /// <summary>
            /// Denne metode henter den valgte boolean fra det valgte Table.
            /// </summary>
            /// <param name="strSql">SQL Query som string</param>
            /// <returns name="bolRes">Bolean - true or false</returns>
            protected bool DbReturnBool(string strSql)
            {
                bool bolRes = false;
                try
                {
                    OpenDB();
                    using (SqlCommand cmd = new SqlCommand(strSql, myConnection))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            bolRes = Convert.ToBoolean(reader.GetValue(0).ToString());
                        }
                    }
                    CloseDB();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return bolRes;
            }
        }
    }
