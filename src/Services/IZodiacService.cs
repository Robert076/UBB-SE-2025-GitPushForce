using System.Threading.Tasks;

namespace Src.Services
{
    public interface IZodiacService
    {
        Task CreditScoreModificationBasedOnJokeAndCoinFlipAsync();
        void CreditScoreModificationBasedOnAttributeAndGravity();
        int ComputeJokeAsciiModulo10(string joke);
        int ComputeGravity();
        bool FlipCoin();
    }
}
