using System.Threading.Tasks;

namespace src.Services
{
    public interface IZodiacService
    {
        public Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync();
        public void CreditScoreModificationBasedOnAttributeAndGravity();
    }
}
