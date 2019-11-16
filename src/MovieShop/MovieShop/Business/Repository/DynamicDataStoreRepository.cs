using EPiServer.Data;
using EPiServer.Data.Dynamic;
using System.Linq;

namespace MovieShop.Business.Repository
{
    public class DynamicDataStoreRepository<TDDS> where TDDS : IDynamicData
    {
        private readonly DynamicDataStore _store;

        public DynamicDataStoreRepository()
        {
            _store = DynamicDataStoreFactory.Instance.CreateStore(typeof(TDDS));
        }

        public Identity Insert(TDDS data)
        {
            var identity = _store.Save(data);
            return identity;
        }

        public IOrderedQueryable<TDDS> Items()
        {
            return _store.Items<TDDS>();
        }

        public void Remove(Identity identity)
        {
            _store.Delete(identity);
        }

        public void Remove(TDDS data)
        {
            _store.Delete(data.Id);
        }

        public TDDS Get(Identity identity)
        {
            return _store.Load<TDDS>(identity);
        }
    }
}