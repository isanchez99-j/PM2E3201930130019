using Proyecto3.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto3.Controller
{
    public class DataBase
    {
        readonly SQLiteAsyncConnection dbase;

        public DataBase(string dbpath)
        {
            dbase = new SQLiteAsyncConnection(dbpath);
            dbase.CreateTableAsync<Recibo>();
            dbase.CreateTableAsync<User>();
        }

        public Task<int> ReciboSave(Recibo recibo)
        {
            if (recibo.id != 0)
            {
                return dbase.UpdateAsync(recibo);
            }
            else
            {
                return dbase.InsertAsync(recibo);
            }
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var user = await dbase.Table<User>()
                .Where(u => u.Username == username && u.Password == password)
                .FirstOrDefaultAsync();

            return user != null;
        }

        public async Task<bool> RegisterAsync(string username, string password)
        {
            var user = await dbase.Table<User>()
                .Where(u => u.Username == username)
                .FirstOrDefaultAsync();

            if (user != null)
            {
                // a user with this username already exists
                return false;
            }
            else
            {
                // create a new user and insert it into the database
                user = new User
                {
                    Username = username,
                    Password = password
                };

                await dbase.InsertAsync(user);
                return true;
            }
        }


        public Task<List<Recibo>> TodosRecibos()
        {
            return dbase.Table<Recibo>().OrderByDescending(recibo => recibo.id).ToListAsync();
        }

        public async Task<Recibo> UnoRecibo(int pid)
        {
            return await dbase.Table<Recibo>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }

        public async Task<int> DeleteRecibo(Recibo recibo)
        {
            return await dbase.DeleteAsync(recibo);
        }
    }
}
