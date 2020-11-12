using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRedis
    {
        /// <summary>
        /// Connection Bilgisi Vermek İçin Kullanılır.
        /// </summary>
        /// <returns></returns>
        ConnectionMultiplexer Get_Connection_Multiplexer();
        /// <summary>
        /// Redis Sunucusuna Bağlanmak İçin Kullanılır.
        /// </summary>
        /// <returns></returns>
        bool Connect();
        /// <summary>
        /// Bağlanılacak Database Context'ini Geriye Döner
        /// </summary>
        /// <param name="id">Database Id</param>
        /// <returns></returns>
        IDatabase Get_Database(int id = 0);
        /// <summary>
        /// Rediste Bulunan Bir Key Değerini Almak İçin Kullanılır.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        Task<string> Get(string key, int db = 0);
        /// <summary>
        /// Rediste Bulunan Bir Key Değeri Class Olarak Almak için Kullanılır.
        /// </summary>
        /// <typeparam name="T">Geriye Dönecek Nesnenin Modeli</typeparam>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key</param>
        /// <returns></returns>
        Task<T> Get<T>(string key, int db = 0);
        /// <summary>
        /// Redis'e Bir Değer Set Etmek İçin Kullanılır.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key Değeri</param>
        /// <param name="value">Redis Key'e Karşılık Gelecek Değer</param>
        /// <param name="expire_date">Geçerlilik Süresi Opsiyoneldir</param>
        /// <returns></returns>
        Task<bool> Set(string key, string value, DateTime? expire_date, int db = 0);
        /// <summary>
        /// Redis'e Bir Class Set Etmek İçin Kullanılır.
        /// </summary>
        /// <typeparam name="T">Redis Key'e Konulacak Sınıfın Tipi</typeparam>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key Değeri</param>
        /// <param name="value">Redis Key'e Karşı Gelen Sınıf</param>
        /// <param name="expire_date">Geçerlilik Süresi Opsiyoneldir.    </param>
        /// <returns></returns>
        Task<bool> Set<T>(string key, T value, DateTime? expire_date, int db = 0);
        /// <summary>
        /// Key Silmek İçin Kullanılır.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key Değeri</param>
        /// <returns></returns>
        Task<bool> Remove_Key(string key, int db = 0);
        /// <summary>
        /// Hash İçerisinde Bulunan Key Sayısını Verir.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="key">Redis Key Değeri</param>
        /// <returns></returns>
        Task<long> Hash_Count(string key, int db = 0);
        /// <summary>
        /// Hash İçerisine Veri Eklemek İçin Kullanılır
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="hash_key">Hash Key Değeri</param>
        /// <param name="key">Key Değeri</param>
        /// <param name="value">Redis Key'e Karşılık Gelecek Değer</param>
        /// <returns></returns>
        Task<bool> Set_Hash(string hash_key, string key, string value, int db = 0);
        /// <summary>
        /// Hash İçerisine Veri Eklemek İçin Kullanılır.
        /// </summary>
        /// <param name="hash_key"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> Set_Hash(string hash_key, string key, object value, int db = 0);
        /// <summary>
        /// Hash İçerisindeki Değeri Silmek İçin Kullanılır.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="hash_key">Hash Key Değeri</param>
        /// <param name="key">Key Değeri</param>
        /// <returns></returns>
        Task<bool> Delete_Hash(string hash_key, string key, int db = 0);
        /// <summary>
        /// Hash İçerisindeki Değeri Getirmek İçin Kullanılır.
        /// </summary>
        /// <param name="db">Database Id</param>
        /// <param name="hash_key">Redis Key Değeri</param>
        /// <returns></returns>
        Task<HashEntry[]> Get_Hash(string hash_key, int db = 0);
        /// <summary>
        /// Redis içerisindeki hashset değerini modele cast eder.
        /// </summary>
        /// <typeparam name="T">Geri Dönülecek Olan Model</typeparam>
        /// <param name="db">Database Id</param>
        /// <param name="hash_key">Hash Key</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        Task<T> Get_Hash<T>(string hash_key, string key, int db = 0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hash_key"></param>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<string> Get_Hash_String(string hash_key, string key, int db = 0);
        /// <summary>
        /// PubSub yapısı oluşturulur
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        ISubscriber Get_Subscriber(int db = 0);
        /// <summary>
        /// Script Çalıştırmak İçin Kullanılır
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> Hash_Increment(string hash_key, string key, int value, int db = 0);

    }
}
