using SwiftCode.BBS.IRepositories;

namespace SwiftCode.BBS.Repositories;

public class CalculateRepository : ICalculateRepository
{
    public int Sum(int i, int j)
    {
        return i + j;
    }
}
