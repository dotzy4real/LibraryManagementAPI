using LibraryManagement.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services.Contracts
{
    public interface IOAuthAuthenticationService
    {
        void AddOAuthAuthentication(OAuthAuthentication oauthAuthentication);

        void UpdateOAuthAuthentication(OAuthAuthentication oauthAuthentication);
        OAuthAuthentication GetById(int id);
        IEnumerable<OAuthAuthentication> GetAllOAuthAuthentications();
        void DeleteOAuthAuthentication(int id);
    }
}
