using System;
using System.Data.SqlClient;

namespace POCSentier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // Me connecter
                // - LE chemin vers le serveur
                // - Les credentials
                // - Le nom de la DB
                // ==> ConnectionString
                string ConnectionString = @"Data Source=MIKEW10;Initial Catalog=SentierDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            // Il me faut un objet qui va permettre la connexion sql
            // ==> SqlConnection
            using (SqlConnection oConn = new SqlConnection(ConnectionString))
            {
                // Ouvrir la connection
                try
                {
                    oConn.Open();

                    // Ecrire ma requête sql
                    string requete = @"SELECT TraductionVertu.Nom, CodeIso
                                        FROM TraductionVertu, Langues
                                        WHERE TraductionVertu.IdLangue = Langues.IdLangue
                                        AND
                                        CodeIso = 'EN-US'
                                        ORDER BY Nom ASC";
                    // Exécuter ma requête sql
                    // J'ai besoin d'un objet qui va véhiculer ma requete vers le serveur via la connexion
                    //==>SqlCommand
                    SqlCommand oCmd = new SqlCommand(requete, oConn); //J'ai mis les gosse dans la voiture
                    //JE dois démarrer==> Exécuter ma requete
                    SqlDataReader oDr = oCmd.ExecuteReader();
                    while (oDr.Read())
                    {
                        // Parcourir le résultat de ma requête sql
                        Console.WriteLine($"{oDr["Nom"]} - {oDr["CodeIso"]}");
                    }
                    //CE que j'ouvre je FERME!!!
                    oDr.Close();
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    //Si ma connection n'est pas fermée, je la FERME!!!!!
                    if(oConn.State!= System.Data.ConnectionState.Closed)
                    {
                        try
                        {
                            oConn.Close();
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }
                    }
                }

            }
        }
    }
}
