using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Utility;

namespace DAL
{
    public class SmsRepository : ISmsRepository
    {
        public bool SendRandomNumber(string mobile, string randomNumber, BaseServiceConfig config)
        {
            try
            {
                var resualtSendActivateCode =
                    ClientHelper.GetScalarValue<string>(config.SendRandomNumberRelativeAddress + $"{mobile}/{randomNumber}", config);
                if (resualtSendActivateCode == null)
                    resualtSendActivateCode = string.Empty;
                return resualtSendActivateCode != string.Empty;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public string GetRandomNumber(string mobile)
        {
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                var randomNumber = con.QueryFirstOrDefault<string>(StaticValue.SpGetRandomNumber,
                    new { mobile = mobile },
                    commandType: CommandType.StoredProcedure
                );
                return randomNumber;
            }

        }

        public bool CheckRandomNumber(string mobile, string randomNumber)
        {
            using (var con = new SqlConnection(StaticValue.SupplementaryConnectionString))
            {
                var resualt = con.QueryFirstOrDefault<int>(StaticValue.SpCheckRandomNumber,
                    new { mobile = mobile, randomNumber = randomNumber },
                    commandType: CommandType.StoredProcedure
                );
                return resualt == 1;
            }
        }
    }

    public interface ISmsRepository
    {
        bool SendRandomNumber(string mobile, string randomNumber, BaseServiceConfig config);

        string GetRandomNumber(string mobile);
        bool CheckRandomNumber(string mobile, string randomNumber);
    }
}
