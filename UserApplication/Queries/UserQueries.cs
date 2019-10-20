using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using UserRepository.Model;

namespace UserApplication.Queries
{
    /// <summary>
    /// CQRS中的Q 是查询的意思
    /// 是直接访问数据库查询，不走Repository
    /// </summary>
    public class UserQueries : IUserQueries
    {
        private string _connectionString = string.Empty;
        public UserQueries(string connStr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(connStr) ? connStr : throw new ArgumentNullException(nameof(connStr));
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<User>("select * from usering.users");
            }
        }
    }
}
