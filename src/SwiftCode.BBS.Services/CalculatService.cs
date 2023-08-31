using SwiftCode.BBS.IServices;

namespace SwiftCode.BBS.Services;

public class CalculatService : ICalculatService
{
    public int Sum(int i, int j)
    {
        return i + j;
    }
}
