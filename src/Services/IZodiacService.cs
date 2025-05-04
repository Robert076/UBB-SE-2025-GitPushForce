using System.Threading.Tasks;

namespace Src.Services
{
    public interface IZodiacService
    {
        public Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync();
        public void CreditScoreModificationBasedOnAttributeAndGravity();
    }
}
