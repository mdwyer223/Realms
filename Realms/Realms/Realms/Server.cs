using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using MySql.Data.MySqlClient;

namespace Realms
{
    public class Server
    {
        MySqlConnection connection;

        protected string uid, password, database, serverName, path;
        protected int connectionTimer = 0, timeOut = 1000;

        protected string message;

        public string Message
        {
            get { return message; }
        }

        public Server(string user, string pass, string db, string server)
        {
            this.uid = user;
            this.password = pass;
            this.database = db;
            this.serverName = server;
            message = "";

            path = "server=" + serverName + ";uid=" + uid + ";pwd=" + password + ";database=" + database;

            connection = new MySqlConnection(path);
            Open();
        }

        public void Open()
        {
            if (connection.State == ConnectionState.Closed)
            {
                try
                {
                    connection.Open();
                    message = "Connected";
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 0:
                            {
                                message = "Can't connect";
                                break;
                            }
                    }

                    if (connectionTimer >= timeOut)
                    {
                        message = "Connection timed out";
                        return;
                    }

                    if (message == "")
                    {
                        connectionTimer += 2;
                        Open();
                    }
                }
            }
        }

        public void changeDB(string dbName)
        {
            connection.Close();
            database = dbName;
            connection = new MySqlConnection(path);
            Open();
        }
    }
}
