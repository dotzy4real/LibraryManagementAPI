using LibraryManagement.Data.DataAccess;
using LibraryManagement.Data.Entity;
using LibraryManagement.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Infrastructure
{
    public class OAuthAuthenticationService : IOAuthAuthenticationService
    {
        private readonly IRepository<OAuthAuthentication> _oauthAuthentication;
        private readonly IUnitOfWork _unitOfWork;
        public OAuthAuthenticationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _oauthAuthentication = unitOfWork.Repository<OAuthAuthentication>();
        }

        public void AddOAuthAuthentication(OAuthAuthentication oauthAuthentication)
        {
            _oauthAuthentication.Insert(oauthAuthentication);
            _unitOfWork.Commit();
        }

        public void UpdateOAuthAuthentication(OAuthAuthentication oauthAuthentication)
        {
            _oauthAuthentication.Update(oauthAuthentication);
            _unitOfWork.Commit();
        }

        public OAuthAuthentication GetById(int id)
        {
            var oauthAuthentication = _oauthAuthentication.GetById(id);
            return oauthAuthentication;
        }

        public IEnumerable<OAuthAuthentication> GetAllOAuthAuthentications()
        {
            var oauthAuthentications = _oauthAuthentication.Get(null, x => x.OrderBy(y => y.Id));
            return oauthAuthentications;
        }

        public void DeleteOAuthAuthentication(int id)
        {
            _oauthAuthentication.Delete(id);
            _unitOfWork.Commit();
        }
    }
}
