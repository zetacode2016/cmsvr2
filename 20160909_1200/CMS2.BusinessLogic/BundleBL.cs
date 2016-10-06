using CMS2.BusinessLogic;
using CMS2.DataAccess.Interfaces;
using CMS2.Entities;

namespace CMS2.BusinessLogic
{
    public class BundleBL : BaseAPCargoBL<Bundle>
    {
        private ICmsUoW _unitOfWork;

        public BundleBL()
        {
            _unitOfWork = GetUnitOfWork();
        }

        public BundleBL(ICmsUoW unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
