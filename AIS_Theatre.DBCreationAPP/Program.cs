using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DBCreationAPP
{
    class Program
    {
        private static string dbName = "theatre_db";
		
        private static bool success = true;
		
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Creating Database...");
                CreateDB();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating GENRE table...");
                CreateGenre();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Filling GENRE table...");
                FillGenre();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating AUTHOR table...");
                CreateAuthor();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating COMPOSITION table...");
                CreateComposition();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating SETTING table...");
                CreateSetting();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating PERFORMANCE table...");
                CreatePerformance();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating ACTOR table...");
                CreateActor();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating ACTOR IN PERFORMANCE table...");
                CreateActorInPerformance();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating MUSICIAN table...");
                CreateMusician();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating MUSICIAN IN PERFORMANCE table...");
                CreateMusicianInPerformance();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating EMLOYEE POSITION table...");
                CreateEmployeePosition();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating EMPLOYEE table...");
                CreateEmployee();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating EMPLOYEE IN POSITION table...");
                CreateEmployeeInPerformance();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating SEASON table...");
                CreateSeason();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating SUBSCRIPTION table...");
                CreateSubscription();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating TICKET table...");
                CreateTicket();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Creating USER table...");
                CreateUser();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }
            try
            {
                Console.WriteLine("Filling USER table...");
                FillUser();
                Console.WriteLine("Succeeded!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                success = false;
                goto end;
            }

end:
            if (success)
            {
                Console.WriteLine("Successful creation of the database!. Press ENTER");
            }
            else
            {
                Console.WriteLine("Database creation failed!");
            }
            Console.ReadKey();
        }

        static void CreateDB()
        {
            string connStr = "Server = localhost; Port = 5432; User Id = postgres; Password = Masterkey;";
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(String.Format(@"
                                       CREATE DATABASE {0}
                                        WITH OWNER = postgres
                                        ENCODING = 'UTF8'
                                        TABLESPACE = pg_default
                                        CONNECTION LIMIT = -1;
                                       ", dbName), m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }
        
        static void CreateGenre()
        {
            string query = "CREATE TABLE public.genre ( id_genre text NOT NULL, name_genre text NOT NULL, CONSTRAINT pk_genre PRIMARY KEY(id_genre) ) WITH( OIDS = FALSE ); ALTER TABLE public.genre OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void FillGenre()
        {
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            string query = "INSERT INTO genre (id_genre, name_genre) VALUES ('0daa890f-c2b4-46a9-ba86-f6b70216eb7e', 'Comedy');";
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('1489f738-3472-4170-b57e-60508faf7344', 'Slice of Life');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('15491da9-cd38-450b-b311-bcc7818ed2bb', 'Historical Fiction');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('1b63a86e-80e1-45b4-870d-3eb551e25222', 'Fantasy');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('21e1b66b-3ed0-448e-bff9-88acc2978b82', 'Philosophical');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('2aeeb061-3f8a-4865-bd65-11aca1c21800', 'Mystery');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('38644fc5-a9bf-4d99-9c5d-c7c0228342a2', 'Romance');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('470d2472-1d86-472b-b0a1-eea1213602ef', 'Horror');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('698c1f28-e8c4-456f-b150-4fca70a6af8a', 'Paranoid');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('7fe4e243-da1e-4aa6-90d8-fdda7faf2e61', 'Western');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('85f102ef-df53-4d02-9a8a-a239de933c17', 'Saga');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('90f45c90-deb7-4218-8046-1bd41ee38e74', 'Adventure');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('af8e42cd-697b-420e-b2af-41a95d13f6dd', 'Magical Realism');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('b3197070-0803-45ef-a0f6-80bd91923c34', 'Political');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('b58a3264-7850-43f2-922f-ba4d791ccaa5', 'Crime');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('b7e59b14-2cbd-4151-b045-59275dd4def4', 'Historical');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('b9690513-b0b0-47a9-96d5-c97f8f4594cb', 'Science Fiction');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('c992ee44-acaa-40d1-9d10-b664243bdc74', 'Drama');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('d293dd12-e378-47ac-b229-0ca9b56f701a', 'Speculative');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('d3d4da0f-ff3e-461f-8ba3-a5e406f76f10', 'Satire');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('f659f7ef-966a-430a-bae0-d7efcc81decc', 'Action');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('f8b80b93-cc01-4ce0-bfad-dc196e484f87', 'Thriller');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
            query = "INSERT INTO genre (id_genre, name_genre) VALUES ('ffbf30dc-1c53-4cee-bd7d-d1536d150f28', 'Urban');";
            m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateAuthor()
        {
            string query = "CREATE TABLE public.author ( id_author text NOT NULL, full_name_author text NOT NULL, birth_date_author text NOT NULL, death_date_author text, country_author text, id_genre_author text NOT NULL, CONSTRAINT pk_author PRIMARY KEY(id_author), CONSTRAINT fk_author_genre FOREIGN KEY(id_genre_author) REFERENCES public.genre(id_genre) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.author OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateComposition()
        {
            string query = "CREATE TABLE public.composition ( id_composition text NOT NULL, name_composition text NOT NULL, year_composition integer NOT NULL, id_author_composition text NOT NULL, id_genre_composition text NOT NULL, CONSTRAINT pk_composition PRIMARY KEY(id_composition), CONSTRAINT pk_composition_author FOREIGN KEY(id_author_composition) REFERENCES public.author(id_author) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT pk_composition_genre FOREIGN KEY(id_genre_composition) REFERENCES public.genre(id_genre) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.composition OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateSetting()
        {
            string query = "CREATE TABLE public.setting ( id_setting text NOT NULL, name_setting text NOT NULL, premiere_date_setting date, id_composition_setting text NOT NULL, CONSTRAINT pk_setting PRIMARY KEY(id_setting), CONSTRAINT fk_setting_composition FOREIGN KEY(id_composition_setting) REFERENCES public.composition(id_composition) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.setting OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreatePerformance()
        {
            string query = "CREATE TABLE public.performance ( id_performance text NOT NULL, date_performance date NOT NULL, id_setting_performance text NOT NULL, time_performance text NOT NULL, CONSTRAINT pk_performance PRIMARY KEY(id_performance), CONSTRAINT fk_performance_setting FOREIGN KEY(id_setting_performance) REFERENCES public.setting(id_setting) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.performance OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateActor()
        {
            string query = "CREATE TABLE public.actor(id_actor text NOT NULL, full_name_actor text NOT NULL, birth_date_actor text NOT NULL, salary_actor integer NOT NULL, experience_actor text NOT NULL, height_actor integer NOT NULL, voice_actor text NOT NULL, gender_actor text NOT NULL, skin_color_actor text NOT NULL, hair_color_actor text NOT NULL, id_stunt_actor text, CONSTRAINT pk_actor PRIMARY KEY(id_actor), CONSTRAINT fk_actor_stunt FOREIGN KEY(id_stunt_actor) REFERENCES public.actor(id_actor) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.actor OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateActorInPerformance()
        {
            string query = "CREATE TABLE public.actor_performance ( id_actor_performance text NOT NULL, id_actor_actor_performance text NOT NULL, id_performance_actor_performance text NOT NULL, CONSTRAINT pk_actor_performance PRIMARY KEY(id_actor_performance), CONSTRAINT fk_actor_performance_actor FOREIGN KEY(id_actor_actor_performance) REFERENCES public.actor(id_actor) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT fk_performance_actor_performance FOREIGN KEY(id_performance_actor_performance) REFERENCES public.performance(id_performance) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.actor_performance OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateMusician()
        {
            string query = "CREATE TABLE public.musician ( id_musician text NOT NULL, full_name_musician text NOT NULL, birth_date_musician text NOT NULL, salary_musician integer NOT NULL, experience_musician text NOT NULL, musical_instrument_musician text NOT NULL, CONSTRAINT pk_musician PRIMARY KEY(id_musician) ) WITH( OIDS = FALSE ); ALTER TABLE public.musician OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateMusicianInPerformance()
        {
            string query = "CREATE TABLE public.musician_performance ( id_musician_performance text NOT NULL, id_musician_musician_performance text NOT NULL, id_performance_musician_performance text NOT NULL, CONSTRAINT pk_musician_performance PRIMARY KEY(id_musician_performance), CONSTRAINT fk_musician_performance_musician FOREIGN KEY(id_musician_musician_performance) REFERENCES public.musician(id_musician) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT fk_musician_performance_performance FOREIGN KEY(id_performance_musician_performance) REFERENCES public.performance(id_performance) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.musician_performance OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateEmployeePosition()
        {
            string query = "CREATE TABLE public.employee_position ( id_employee_position text NOT NULL, name_employee_position text NOT NULL, CONSTRAINT pk_employee_position PRIMARY KEY(id_employee_position) ) WITH( OIDS = FALSE ); ALTER TABLE public.employee_position OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateEmployee()
        {
            string query = "CREATE TABLE public.employee ( id_employee text NOT NULL, full_name_employee text NOT NULL, birth_date_employee text NOT NULL, salary_employee integer NOT NULL, experience_employee text NOT NULL, id_employee_position_employee text NOT NULL, CONSTRAINT pk_employee PRIMARY KEY(id_employee), CONSTRAINT fk_employee_employee_position FOREIGN KEY(id_employee_position_employee) REFERENCES public.employee_position(id_employee_position) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.employee OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateEmployeeInPerformance()
        {
            string query = "CREATE TABLE public.employee_performance ( id_employee_performance text NOT NULL, id_employee_employee_performance text NOT NULL, id_performance_employee_performance text NOT NULL, CONSTRAINT pk_employee_performance PRIMARY KEY(id_employee_performance), CONSTRAINT fk_employee_performance_employee FOREIGN KEY(id_employee_employee_performance) REFERENCES public.employee(id_employee) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT fk_employee_performance_performance FOREIGN KEY(id_performance_employee_performance) REFERENCES public.performance(id_performance) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.employee_performance OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateSeason()
        {
            string query = "CREATE TABLE public.season ( id_season text NOT NULL, begin_date_season date NOT NULL, end_date_season date NOT NULL, CONSTRAINT pk_season PRIMARY KEY(id_season) ) WITH( OIDS = FALSE ); ALTER TABLE public.season OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateSubscription()
        {
            string query = "CREATE TABLE public.subscription ( id_subscription text NOT NULL, id_season_subscription text NOT NULL, CONSTRAINT pk_subscription PRIMARY KEY(id_subscription), CONSTRAINT fk_subscription_season FOREIGN KEY(id_season_subscription) REFERENCES public.season(id_season) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.subscription OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateTicket()
        {
            string query = "CREATE TABLE public.ticket ( id_ticket text NOT NULL, price_ticket integer NOT NULL, row_ticket text NOT NULL, place_ticket integer NOT NULL, is_free_ticket boolean NOT NULL, id_performance_ticket text NOT NULL, id_subscription_ticket text, CONSTRAINT pk_ticket PRIMARY KEY(id_ticket), CONSTRAINT fk_ticket_performance FOREIGN KEY(id_performance_ticket) REFERENCES public.performance(id_performance) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION, CONSTRAINT fk_ticket_subscription FOREIGN KEY(id_subscription_ticket) REFERENCES public.subscription(id_subscription) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION ) WITH( OIDS= FALSE ); ALTER TABLE public.ticket OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void CreateUser()
        {
            string query = "CREATE TABLE public.user_account ( id_user text NOT NULL, login_user text NOT NULL, password_user text NOT NULL, CONSTRAINT pk_user PRIMARY KEY(id_user) ) WITH( OIDS = FALSE ); ALTER TABLE public.user_account OWNER TO postgres;";
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }

        static void FillUser()
        {
            string connStr = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=Masterkey;Database={0};", dbName);
            NpgsqlConnection m_conn = new NpgsqlConnection(connStr);
            string query = "INSERT INTO user_account (id_user, login_user, password_user) VALUES ('BA4BF23D-ADD1-4EED-9DE0-13C082FAD0F5', 'mail@mail.ru', 'Entity');";
            NpgsqlCommand m_createdb_cmd = new NpgsqlCommand(query, m_conn);
            m_conn.Open();
            m_createdb_cmd.ExecuteNonQuery();
            m_conn.Close();
        }
    }
}
