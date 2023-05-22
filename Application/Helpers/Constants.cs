using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;

namespace Application.Helpers
{
    public class Constants : IConstants
    {
        public Constants() { }

        public string getSecret() {
            return Environment.GetEnvironmentVariable("API_SECRET");
        }

        public string getConnectionString()
        {
            //TODO: Make this so it test if the variables are empty, as the api should not be started without em
            string sqlIp = this.getEnvironmentString("SQL_SERVER_IP");
            string sqlPort = this.getEnvironmentString("SQL_SERVER_PORT");
            string sqlDatabase = this.getEnvironmentString("SQL_DATABASE");
            string sqlUid = this.getEnvironmentString("SQL_USER");
            string sqlPwd = this.getEnvironmentString("SQL_PASS");

            var connectionStringTemplate = "Server={0};Port={1};Database={2};Uid={3};Pwd={4}";
            string[] data = { sqlIp, sqlPort, sqlDatabase, sqlUid, sqlPwd };
            string st = string.Format(connectionStringTemplate, data);

            Console.WriteLine(st);

            return st;
        }

        public string getSerilog(){
            string serverIp = this.getEnvironmentString("SQL_SERVER_IP");
            string serilogPort = "9200";
            var connectionString = "https://{0}:{1}";
            string[] data = { serverIp, serilogPort };
            string st = string.Format(connectionString, data);
            return st;
        }

        private string getEnvironmentString(string envVariable)
        {
            try
            {
                string st = Environment.GetEnvironmentVariable(envVariable);
                return st;
            }
            catch (System.Exception)
            {

                throw;
            }
            throw new Exception("Unhandled getEnvStr");
        }
    }
}