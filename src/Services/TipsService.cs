using Src.Model;
using Src.Repos;
using Src.Services;
using System.Collections.Generic;
using System;

public class TipsService : ITipsService
{
    private readonly ITipsRepository tipsRepository;
    private readonly IUserRepository userRepository;

    public TipsService(ITipsRepository tipsRepository, IUserRepository userRepository)
    {
        this.tipsRepository = tipsRepository;
        this.userRepository = userRepository;
    }

    public void GiveTipToUser(string userCNP)
    {
        try
        {
            int userCreditScore = userRepository.GetUserByCnp(userCNP).CreditScore;
            if (userCreditScore < 300)
            {
                tipsRepository.GiveUserTipForLowBracket(userCNP);
            }
            else if (userCreditScore < 550)
            {
                tipsRepository.GiveUserTipForMediumBracket(userCNP);
            }
            else if (userCreditScore > 549)
            {
                tipsRepository.GiveUserTipForHighBracket(userCNP);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"{exception.Message}, User is not found");
        }
    }

    public List<Tip> GetTipsForGivenUser(string userCnp)
    {
        return tipsRepository.GetTipsForGivenUser(userCnp);
    }
}
