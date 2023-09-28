using DAL;
using Model;

namespace Service
{
    public class DatabaseService
    {
        private BaseDao dao;

        public DatabaseService()
        {
            dao = new BaseDao();
        }
        
        public List<DatabasesModel> Get_All_Databases()
        {
            return dao.GetDatabases();
        }
    }
}