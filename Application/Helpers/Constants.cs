using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Applicatoin.Interfaces;

namespace Application.Helpers
{
    public class Constants : IConstants
    {
        public Constants()
        {

        }

        public string getConnectionString()
        {
            string sqlIp = this.getEnvironmentString("SQL_SERVER_IP");
            string sqlPort = this.getEnvironmentString("SQL_SERVER_PORT");
            string sqlDatabase = this.getEnvironmentString("SQL_DATABASE");
            string sqlUid = this.getEnvironmentString("SQL_USER");
            string sqlPwd = this.getEnvironmentString("SQL_PASS");

            var connectionStringTemplate = "Server={0};Port={1};Database={2};Uid={3};Pwd={4}";
            string[] data = { sqlIp, sqlPort, sqlDatabase, sqlUid, sqlPwd };
            string st = string.Format(connectionStringTemplate, data);

            return st;
        }

        private string getEnvironmentString(string envVariable)
        {
            try{
                string st = Environment.GetEnvironmentVariable(envVariable);
                return st;
            } catch (System.Exception) {

                throw;
            }
            throw new Exception("Unhandled getEnvStr");
        }
    }
}