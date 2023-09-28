using DAL;
using Model;

namespace Service
{
    public class Databases
    {
        private BaseDao dao;

        public Databases()
        {
            dao = new BaseDao();
        }
        
        public List<DatabasesModel> Get_All_Databases()
        {
            return dao.GetDatabases();
        }
    }
}