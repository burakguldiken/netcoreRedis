using Business.Interfaces;
using DataAccess.Connections;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Business.Services
{
    public class SRedis : Business.Interfaces.IRedis
    {
        private ConnectionMultiplexer _redis;

        public ConnectionMultiplexer Get_Connection_Multiplexer()
        {
            return _redis;
        }

        public SRedis()
        {
            Connect();
        }

        public bool Connect()
        {
            bool _rtn = true;
            try
            {
                Connection connection = Connection.Instance;
                string connString = $"{connection.redisHost}";
                _redis = ConnectionMultiplexer.Connect(connString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _rtn = false;
            }
            return _rtn;
        }

        public IDatabase Get_Database(int id)
        {
            IDatabase _rtn = null;
            try
            {
                _rtn = _redis.GetDatabase(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _rtn;
        }

        public async Task<string> Get(string key, int db)
        {
            string _rtn = "";
            try
            {
                IDatabase rdb = Get_Database(db);
                _rtn = await rdb.StringGetAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _rtn;
        }

        public async Task<T> Get<T>(string key, int db)
        {
            T _rtn = default;
            try
            {
                IDatabase rdb = Get_Database(db);
                string value = await rdb.StringGetAsync(key);
                if (!String.IsNullOrEmpty(value))
                    _rtn = JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return _rtn;
        }

        public async Task<bool> Set(string key, string value, DateTime? expire_date, int db)
        {
            bool _rtn = true;
            try
            {
                IDatabase rdb = Get_Database(db);
                if (expire_date != null)
                {
                    var expiryTimeSpan = expire_date.Value.Subtract(DateTime.UtcNow);
                    await rdb.StringSetAsync(key, value, expiryTimeSpan);
                }
                else
                {
                    await rdb.StringSetAsync(key, value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                _rtn = false;
            }
            return _rtn;
        }

        public async Task<bool> Set<T>(string key, T value, DateTime? expire_date, int db)
        {
            string _value = JsonConvert.SerializeObject(value);
            return await Set(key, _value, expire_date, db);
        }

        public async Task<bool> Remove_Key(string key, int db)
        {
            bool rtn = true;
            try
            {
                IDatabase rdb = Get_Database(db);
                await rdb.KeyDeleteAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rtn = false;
            }
            return rtn;
        }

        public async Task<long> Hash_Count(string key, int db)
        {
            long rtn = 0;
            try
            {
                IDatabase rdb = Get_Database(db);
                rtn = await rdb.HashLengthAsync(key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtn;
        }

        public async Task<bool> Set_Hash(string hash_key, string key, string value, int db)
        {
            bool rtn = true;
            try
            {
                IDatabase rdb = Get_Database(db);
                await rdb.HashSetAsync(hash_key, key, value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rtn = false;
            }
            return rtn;
        }

        public async Task<bool> Set_Hash(string hash_key, string key, object value, int db = 0)
        {
            return await Set_Hash(hash_key, key, JsonConvert.SerializeObject(value), db);
        }

        public async Task<bool> Delete_Hash(string hash_key, string key, int db)
        {
            bool rtn = true;
            try
            {
                IDatabase rdb = Get_Database(db);
                await rdb.HashDeleteAsync(hash_key, key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rtn = false;
            }
            return rtn;
        }

        public async Task<HashEntry[]> Get_Hash(string hash_key, int db)
        {
            HashEntry[] rtn = null;
            try
            {
                IDatabase rdb = Get_Database(db);
                rtn = await rdb.HashGetAllAsync(hash_key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtn;
        }

        public async Task<T> Get_Hash<T>(string hash_key, string key, int db)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            try
            {
                IDatabase rdb = Get_Database(db);
                string json = await rdb.HashGetAsync(hash_key, key);
                if (!String.IsNullOrEmpty(json))
                    result = JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public async Task<string> Get_Hash_String(string hash_key, string key, int db = 0)
        {
            string result = null;
            try
            {
                IDatabase rdb = Get_Database(db);
                result = await rdb.HashGetAsync(hash_key, key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        public ISubscriber Get_Subscriber(int db)
        {
            ISubscriber rtn = null;
            try
            {
                rtn = _redis.GetSubscriber(db);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtn;
        }

        public async Task<bool> Hash_Increment(string hash_key, string key, int value, int db = 0)
        {
            bool rtn = false;
            try
            {
                IDatabase rdb = Get_Database(db);
                await rdb.HashIncrementAsync(hash_key, key, value);
                rtn = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return rtn;
        }
    }
}
