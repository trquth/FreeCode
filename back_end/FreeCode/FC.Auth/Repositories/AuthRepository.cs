using FC.Auth.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FC.Auth.Repositories
{
    public class AuthRepository : IDisposable
    {
        private AuthContext _ctx;
        public AuthRepository()
        {
            _ctx = new AuthContext();
        }
        public Client FindClient(string clientId)
        {
            using (AuthContext _ctx = new AuthContext())
            {
                var client = _ctx.Clients.Find(clientId);
                return client;
            }
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            using (AuthContext _ctx = new AuthContext())
            {
                var existingToken = _ctx.RefreshTokens.Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();

                if (existingToken != null)
                {
                    var result = await RemoveRefreshToken(existingToken);
                }

                _ctx.RefreshTokens.Add(token);

                return await _ctx.SaveChangesAsync() > 0;
            }

        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            using (AuthContext _ctx = new AuthContext())
            {
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);

                if (refreshToken != null)
                {
                    _ctx.RefreshTokens.Remove(refreshToken);
                    return await _ctx.SaveChangesAsync() > 0;
                }

                return false;
            }

        }

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            using (AuthContext _ctx = new AuthContext())
            {
                _ctx.RefreshTokens.Remove(refreshToken);
                return await _ctx.SaveChangesAsync() > 0;
            }
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            using (AuthContext _ctx = new AuthContext())
            {
                var refreshToken = await _ctx.RefreshTokens.FindAsync(refreshTokenId);
                return refreshToken;
            }
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            using (AuthContext _ctx = new AuthContext())
            {
                return _ctx.RefreshTokens.ToList();
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}