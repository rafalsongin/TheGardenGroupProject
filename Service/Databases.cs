using DAL;
using Model;

namespace Service
{
    public class Databases
    {
        private Dao dao;

        public Databases()
        {
            dao = new Dao();
        }
        
        public List<Databases_Model> Get_All_Databases()
        {
            return dao.GetDatabases();
        }
    }
}